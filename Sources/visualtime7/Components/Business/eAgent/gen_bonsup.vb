Option Strict Off
Option Explicit On
Public Class gen_bonsup
	'%-------------------------------------------------------%'
	'% $Workfile:: gen_bonsup.cls                           $%'
	'% $Author:: Nvaplat28                                  $%'
	'% $Date:: 26/09/03 12:57                               $%'
	'% $Revision:: 10                                       $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'gen_bonsup' en el sistema 14/12/2001 11:47:11 a.m.
	
	'+       Column name              Type
	'+  ------------------------- ------------
	
	Public nInit_Range As Double
	Public nEnd_Range As Double
	Public nFactor As Double
	Public nUsercode As Integer
	
	'% Update the links for a specific client
	Public Function insUpdGen_Bonsup(ByVal nAction As Integer) As Boolean
		Dim lclsgen_bonsup As eRemoteDB.Execute
		
		lclsgen_bonsup = New eRemoteDB.Execute
		
		On Error GoTo insUpdGen_Bonsup_Err
		
		'+ Define all parameters for the stored procedures 'insudb.updgen_bonsup'. Generated on 14/12/2001 11:47:11 a.m.
		With lclsgen_bonsup
			.StoredProcedure = "insUpdGen_Bonsup"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nInit_Range", nInit_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Range", nEnd_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFactor", nFactor, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdGen_Bonsup = .Run(False)
		End With
		
insUpdGen_Bonsup_Err: 
		If Err.Number Then
			insUpdGen_Bonsup = False
		End If
		'UPGRADE_NOTE: Object lclsgen_bonsup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsgen_bonsup = Nothing
	End Function
	
	'IsExist: Función que realiza la busqueda en la tabla 'insudb.gen_bonsup'
	Public Function IsExist(ByVal nInit_Range As Double, ByVal nEnd_Range As Double) As Boolean
		Dim lclsgen_bonsup As eRemoteDB.Execute
		Dim lblnExist As Boolean
		
		IsExist = False
		
		On Error GoTo IsExist_Err
		lclsgen_bonsup = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.valgen_bonsupExist'. Generated on 14/12/2001 11:47:11 a.m.
		With lclsgen_bonsup
			.StoredProcedure = "Val_Gen_Bonsup_Exist"
			.Parameters.Add("nInit_Range", nInit_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nEnd_Range", nEnd_Range, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
		
		'UPGRADE_NOTE: Object lclsgen_bonsup may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsgen_bonsup = Nothing
		
		On Error GoTo 0
	End Function
	
	'insValMAG750_K: Función que realiza la validacion de los datos introducido en la sección
	'                de detalles de la ventana
	Public Function insValMAG750_K(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nInit_Range As Double, ByVal nEnd_Range As Double, ByVal nFactor As Double) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMAG750_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Rango Inicial: Debe estar lleno
		
		If (nInit_Range = 0 Or nInit_Range = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10182)
		End If
		
		'+ Rango Final: Debe estar lleno
		
		If (nEnd_Range = 0 Or nEnd_Range = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 10183)
		End If
		
		'+ Rango Final: Debe ser mayor al rango inicial
		If nEnd_Range < nInit_Range Then
			Call lclsErrors.ErrorMessage(sCodispl, 10184)
		End If
		
		'+ Factor: Debe estar lleno
		
		If (nFactor = 0 Or nFactor = eRemoteDB.Constants.intNull) Then
			Call lclsErrors.ErrorMessage(sCodispl, 1095)
		End If
		
		'+ Rango Inicial: No puede estar contenido en ningún otro rango.
		
		If (nInit_Range <> 0 And nInit_Range <> eRemoteDB.Constants.intNull) Then
			If sAction = "Add" Then
				If IsExist(nInit_Range, nEnd_Range) Then
					Call lclsErrors.ErrorMessage(sCodispl, 60214)
				End If
			End If
		End If
		
		insValMAG750_K = lclsErrors.Confirm
		
insValMAG750_K_Err: 
		If Err.Number Then
			insValMAG750_K = insValMAG750_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insPostMAG750_K: Función que realiza la validacion de los datos introducido por la ventana
	Public Function insPostMAG750_K(ByVal bHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal nInit_Range As Double, ByVal nEnd_Range As Double, ByVal nFactor As Double) As Boolean
		On Error GoTo insPostMAG750_K_Err
		
		With Me
			.nInit_Range = nInit_Range
			.nEnd_Range = nEnd_Range
			.nFactor = nFactor
			.nUsercode = nUsercode
			
			
			If bHeader Then
				insPostMAG750_K = True
			Else
				
				Select Case sAction
					
					'+ Acción: Agregar
					Case "Add"
						insPostMAG750_K = insUpdGen_Bonsup(1)
						
						'+ Acción: Actualizar
					Case "Update"
						insPostMAG750_K = insUpdGen_Bonsup(2)
						
						'+ Acción: Borrar
					Case "Del"
						insPostMAG750_K = insUpdGen_Bonsup(3)
						
				End Select
				
			End If
			
		End With
		
insPostMAG750_K_Err: 
		If Err.Number Then
			insPostMAG750_K = False
		End If
		On Error GoTo 0
	End Function
End Class






