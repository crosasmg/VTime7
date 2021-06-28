Option Strict Off
Option Explicit On
Public Class Table5708
	'%-------------------------------------------------------%'
	'% $Workfile:: Noconvers.cls                            $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:06p                                $%'
	'% $Revision:: 9                                        $%'
	'%-------------------------------------------------------%'
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ -------------- - -------- ------- ----- ------ --------
	Public nType_Move As Integer ' NUMBER        22     5      0 No
	Public sDescript As String ' CHAR          30              Yes
	Public sShort_des As String ' CHAR          30              Yes
	Public nType As Integer ' NUMBER        22     2      0 Yes
	Public sPb_Bmg As String ' CHAR           1              Yes
	Public sStatregt As String ' CHAR           1              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	'+ Variables de uso de la clase
	Public nActions As Integer
	Public nExists As Integer
	
	'%Find. Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%tabla "Table5708". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Find(ByVal nType_Move As Integer) As Boolean
		Dim lrecTable5708 As eRemoteDB.Execute
		On Error GoTo Find_Err
		lrecTable5708 = New eRemoteDB.Execute
		Find = False
		With lrecTable5708
			.StoredProcedure = "reaTable5708"
			.Parameters.Add("nType_Move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nType_Move = .FieldToClass("nType_Move")
				sDescript = .FieldToClass("sDescript")
				sShort_des = .FieldToClass("sShort_des")
				nType = .FieldToClass("nType")
				sPb_Bmg = .FieldToClass("sPb_Bmg")
				sStatregt = .FieldToClass("sStatregt")
				Find = True
				.RCloseRec()
			End If
		End With
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		lrecTable5708 = Nothing
	End Function
	
	'%Add. Este metodo se encarga de realizar la insercion de los datos correspondientes para la
	'%tabla "Noconvers". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Add() As Boolean
		Dim lrecTable5708 As eRemoteDB.Execute
		On Error GoTo Add_err
		lrecTable5708 = New eRemoteDB.Execute
		With lrecTable5708
			.StoredProcedure = "insTable5708"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPb_Bmg", sPb_Bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		lrecTable5708 = Nothing
	End Function
	
	'%Update. Este metodo se encarga de realizar actualizar de los datos correspondientes para la
	'%tabla "Noconvers". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Update() As Boolean
		Dim lrecTable5708 As eRemoteDB.Execute
		On Error GoTo Update_Err
		lrecTable5708 = New eRemoteDB.Execute
		With lrecTable5708
			.StoredProcedure = "insTable5708"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPb_Bmg", sPb_Bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		lrecTable5708 = Nothing
	End Function
	
	'%Delete. Este metodo se encarga de eliminar los registros  de los datos correspondientes para la
	'%tabla "Noconvers". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Delete() As Boolean
		Dim lrecTable5708 As eRemoteDB.Execute
		On Error GoTo Delete_err
		lrecTable5708 = New eRemoteDB.Execute
		With lrecTable5708
			.StoredProcedure = "insTable5708"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType_Move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sShort_des", sShort_des, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nType", nType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sPb_Bmg", sPb_Bmg, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		lrecTable5708 = Nothing
	End Function
	
	'%insValMCA815: Validación de los campos que son ingresados en la popup de la pagina MCA815
	Public Function insValMVI5708(ByVal sCodispl As String, ByVal sActions As String, ByVal nType_Move As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nType As Integer, ByVal sPb_Bmg As String, ByVal sStatregt As String) As String
		Dim lclsErrors As New eFunctions.Errors
		On Error GoTo insValMVI5708_err
		
		'+ Si el campo causa no esta lleno, ninguna de los campos debe estar lleno
		If nType_Move = eRemoteDB.Constants.intNull Or nType_Move = 0 Then
			If sDescript <> String.Empty Or sPb_Bmg <> String.Empty Or (nType <> eRemoteDB.Constants.intNull And nType <> 0) Or (sStatregt <> String.Empty And sStatregt <> "0") Then
				Call lclsErrors.ErrorMessage(sCodispl, 1084)
			End If
		End If
		
		'    If sActions = "Del" Then
		'        If Find_Type_Move(nType_Move) Then
		'            Call lclsErrors.ErrorMessage(sCodispl, 55873)
		'        End If
		'    End If
		
		If sActions = "Add" Then
			'+ Si la acción es registrar el campo causa debe estar lleno
			If nType_Move = eRemoteDB.Constants.intNull Or nType_Move = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 7125)
			Else
				'+ si la acción es registrar no debe existir en el sistema (Table5708)
				If Find(nType_Move) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10004)
				End If
			End If
		End If
		
		'+ si el campo causa esta lleno, la descripcion tambien debe estar llena
		If nType_Move <> eRemoteDB.Constants.intNull And nType_Move <> 0 Then
			If sDescript = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10005)
			End If
			
			'+ Si el campo causa esta lleno, el estado debe estar lleno
			If (sStatregt = String.Empty Or sStatregt = "0") Then
				Call lclsErrors.ErrorMessage(sCodispl, 9089)
			End If
		End If
		
		'    If nType = NumNull Or _
		''       nType = 0 Then
		'        Call lclsErrors.ErrorMessage(sCodispl, 60485)
		'    End If
		
		insValMVI5708 = lclsErrors.Confirm
		
insValMVI5708_err: 
		If Err.Number Then
			insValMVI5708 = "Table5708.insValMVI5708: " & Err.Description
		End If
		On Error GoTo 0
		lclsErrors = Nothing
	End Function
	
	'%insPostMVI5708: Actualización de los datos ingresados en las causas pendientes
	Public Function insPostMVI5708(ByVal sActions As String, ByVal nType_Move As Integer, ByVal sDescript As String, ByVal sShort_des As String, ByVal nType As Integer, ByVal sPb_Bmg As String, ByVal sStatregt As String, ByVal nUsercode As Integer) As Boolean
		With Me
			.nType_Move = nType_Move
			.sDescript = sDescript
			.sShort_des = sShort_des
			.nType = nType
			.sPb_Bmg = IIf(sPb_Bmg = "1", "1", "2")
			.sStatregt = sStatregt
			.nUsercode = nUsercode
			Select Case UCase(sActions)
				Case "ADD"
					.nActions = 1
					insPostMVI5708 = .Add()
				Case "UPDATE"
					.nActions = 2
					insPostMVI5708 = .Update()
				Case "DEL"
					.nActions = 3
					insPostMVI5708 = .Delete()
			End Select
		End With
	End Function
	
	'%Find_Type_Move: Busca en la tabla 5708 si ya existe el registro
	Public Function Find_Type_Move(ByVal nType_Move As Integer) As Boolean
		Dim lrecTable5708 As eRemoteDB.Execute
		On Error GoTo Find_Type_Move_Err
		lrecTable5708 = New eRemoteDB.Execute
		'Si nexist = 1 existen datos
		'Si nexist = 2 no existen datos
		With lrecTable5708
			.StoredProcedure = "reaTable5708_Type_Move"
			With .Parameters
				.Add("nType_Move", nType_Move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			If .Run(False) Then
				Find_Type_Move = .Parameters("nExists").Value > 0
			End If
		End With
		
Find_Type_Move_Err: 
		If Err.Number Then
			Find_Type_Move = False
		End If
		On Error GoTo 0
		lrecTable5708 = Nothing
	End Function
	
	'%Class_Initialize: Se ejecuta cuando se instancia un objeto de la clase
	Private Sub Class_Initialize_Renamed()
		nType_Move = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		nType = eRemoteDB.Constants.intNull
		sPb_Bmg = String.Empty
		sStatregt = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






