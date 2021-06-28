Option Strict Off
Option Explicit On
Public Class tab_goals
	'%-------------------------------------------------------%'
	'% $Workfile:: tab_goals.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:41p                                $%'
	'% $Revision:: 7                                        $%'
	'%-------------------------------------------------------%'
	
	'+Propiedades según la tabla 'tab_goals' en el sistema 17/01/2002 09:51:46 a.m.
	
	'+       Column name              Type
	'+  ------------------------- ------------
	
	Public nCode As Double
	Public sDescript As String
	Public sShort_des As String
	Public sStatregt As String
	Public nUsercode As Integer
	
	'% Update the links for a specific client
	Public Function insUpdTab_Goals(ByVal nAction As Integer) As Boolean
		Dim lclstab_goals As eRemoteDB.Execute
		
		lclstab_goals = New eRemoteDB.Execute
		
		On Error GoTo insUpdTab_Goals_Err
		
		'+ Define all parameters for the stored procedures 'insudb.insUpdTab_Goals'. Generated on 17/01/2002 09:51:46 a.m.
		With lclstab_goals
			.StoredProcedure = "insUpdTab_Goals"
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sshort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatRegt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insUpdTab_Goals = .Run(False)
		End With
		
		
insUpdTab_Goals_Err: 
		If Err.Number Then
			insUpdTab_Goals = False
		End If
		'UPGRADE_NOTE: Object lclstab_goals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclstab_goals = Nothing
		On Error GoTo 0
	End Function
	
	'IsExist: Función que realiza la busqueda en la tabla 'insudb.tab_goals'
	Public Function IsExist(ByVal nCode As Double) As Boolean
		Dim lclstab_goals As eRemoteDB.Execute
		
		lclstab_goals = New eRemoteDB.Execute
		
		'+ Define all parameters for the stored procedures 'insudb.valtab_goalsExist'. Generated on 17/01/2002 09:51:46 a.m.
		With lclstab_goals
			.StoredProcedure = "reatab_goals_v"
			.Parameters.Add("nCode", nCode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run() Then
				IsExist = (.FieldToClass("nExist") = 1)
			Else
				IsExist = False
			End If
		End With
		'UPGRADE_NOTE: Object lclstab_goals may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclstab_goals = Nothing
	End Function
	
	'insValMAG7780_K: Función que realiza la validacion de los datos introducidos en la sección
	'                 de detalles de la ventana
	Public Function insValMAG7780_K(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal sDescript As String, ByVal nCode As Double, ByVal sShort_des As String, ByVal sStatregt As String) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo insValMAG7780_K_Err
		
		lclsErrors = New eFunctions.Errors
		
		'+ Código: Debe estar lleno
		
		If nCode = eRemoteDB.Constants.intNull Or nCode = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 1942)
		End If
		
		'+ Descripción: Debe estar llena
		
		If sDescript = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 10857)
		End If
		
		
		'+ Descripción Corta: Debe estar llena
		
		If sShort_des = String.Empty Then
			Call lclsErrors.ErrorMessage(sCodispl, 10858)
		End If
		
		'+ Estado: Debe estar lleno
		If sStatregt = "0" Then
			Call lclsErrors.ErrorMessage(sCodispl, 1016)
		End If
		
		'+ '+ Registro no debe estar repetido
		If sAction = "Add" Then
			If IsExist(nCode) Then
				Call lclsErrors.ErrorMessage(sCodispl, 10284)
			End If
		End If
		
		insValMAG7780_K = lclsErrors.Confirm
		
insValMAG7780_K_Err: 
		If Err.Number Then
			insValMAG7780_K = insValMAG7780_K & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'insPostMAG7780_K: Función que realiza la validacion de los datos introducidos por la ventana
	Public Function insPostMAG7780_K(ByVal bHeader As Boolean, ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal sAction As String, ByVal nUsercode As Integer, ByVal sDescript As String, ByVal nCode As Double, ByVal sShort_des As String, ByVal sStatregt As String) As Boolean
		On Error GoTo insPostMAG7780_K_Err
		
		With Me
			.nCode = nCode
			.sShort_des = sShort_des
			.sStatregt = sStatregt
			.sDescript = sDescript
			.nUsercode = nUsercode
			
			If bHeader Then
				insPostMAG7780_K = True
			Else
				Select Case sAction
					
					'+ Acción: Agregar
					Case "Add"
						insPostMAG7780_K = insUpdTab_Goals(1)
						
						'+ Acción: Actualizar
					Case "Update"
						insPostMAG7780_K = insUpdTab_Goals(2)
						
						'+ Acción: Borrar
					Case "Del"
						insPostMAG7780_K = insUpdTab_Goals(3)
						
				End Select
			End If
			
		End With
		
insPostMAG7780_K_Err: 
		If Err.Number Then
			insPostMAG7780_K = False
		End If
		On Error GoTo 0
	End Function
End Class






