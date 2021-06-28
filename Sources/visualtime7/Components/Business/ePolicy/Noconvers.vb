Option Strict Off
Option Explicit On
Public Class Noconvers
	'%-------------------------------------------------------%'
	'% $Workfile:: Noconvers.cls                            $%'
	'% $Author:: Gletelier                                  $%'
	'% $Date:: 24/08/09 3:58p                               $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'------------------------------ -------------- - -------- ------- ----- ------ --------
	Public nNo_convers As Integer ' NUMBER        22     5      0 No
	Public sDescript As String ' CHAR          30              Yes
	Public nAreaWait As Integer ' NUMBER        22     2      0 Yes
	Public sDevo As String ' CHAR           1              Yes
	Public sDisc As String ' CHAR           1              Yes
	Public sStatregt As String ' CHAR           1              Yes
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	Public nExpenses As Double ' NUMBER        22     2      0 Yes
	Public sRoutine As String ' CHAR          30              Yes
	Public nHealthexp As Double ' NUMBER        22     2      0 Yes
	Public nRoutine As Double ' NUMBER        22     2      0 Yes
	
	'+ Variables de uso de la clase
	Public nActions As Integer
	Public nExists As Integer
	
	'%Find. Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%tabla "Noconvers". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Find(ByVal nNo_convers As Integer) As Boolean
		Dim lrecNoconvers As eRemoteDB.Execute
		On Error GoTo Find_Err
		lrecNoconvers = New eRemoteDB.Execute
		Find = False
		With lrecNoconvers
			.StoredProcedure = "reaNoconvers"
			.Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run Then
				nNo_convers = .FieldToClass("nNo_convers")
				sDescript = .FieldToClass("sDescript")
				nAreaWait = .FieldToClass("nAreaWait")
				sDevo = .FieldToClass("sDevo")
				sDisc = .FieldToClass("sDisc")
				sStatregt = .FieldToClass("sStatregt")
				nExpenses = .FieldToClass("nExpenses")
				sRoutine = .FieldToClass("sRoutine")
				nHealthexp = .FieldToClass("nHealthexp")
				Find = True
				.RCloseRec()
			End If
		End With
Find_Err: 
		If Err.Number Then
			Find = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecNoconvers = Nothing
	End Function
	
	
	'%Find. Este metodo se encarga de realizar la busqueda de los datos correspondientes para la
	'%tabla "Noconvers". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
    Public Function Find_CA099(ByVal nNo_convers As Integer, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal dStat_date As Date = #12:00:00 AM#, Optional ByVal dLimit_date As Date = #12:00:00 AM#, Optional ByVal dDate_init As Date = #12:00:00 AM#) As Boolean
        Dim lrecNoconvers As eRemoteDB.Execute
        On Error GoTo Find_Err
        lrecNoconvers = New eRemoteDB.Execute
        Find_CA099 = False
        With lrecNoconvers
            .StoredProcedure = "reaNoconversCA099"
            .Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStat_date", dStat_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dLimit_date", dLimit_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDate_init", dDate_init, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                nNo_convers = .FieldToClass("nNo_convers")
                sDescript = .FieldToClass("sDescript")
                nAreaWait = .FieldToClass("nAreaWait")
                sDevo = .FieldToClass("sDevo")
                sDisc = .FieldToClass("sDisc")
                sStatregt = .FieldToClass("sStatregt")
                nExpenses = .FieldToClass("nExpenses")
                sRoutine = .FieldToClass("sRoutine")
                nHealthexp = .FieldToClass("nHealthexp")
                nRoutine = .FieldToClass("nRoutine")
                Find_CA099 = True
                .RCloseRec()
            End If
        End With
Find_Err:
        If Err.Number Then
            Find_CA099 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecNoconvers = Nothing
    End Function
	
	'%Add. Este metodo se encarga de realizar la insercion de los datos correspondientes para la
	'%tabla "Noconvers". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Add() As Boolean
		Dim lrecNoconvers As eRemoteDB.Execute
		On Error GoTo Add_err
		lrecNoconvers = New eRemoteDB.Execute
		With lrecNoconvers
			.StoredProcedure = "insNoconvers"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAreaWait", nAreaWait, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDevo", sDevo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDisc", sDisc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExpenses", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHealthexp", nHealthexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecNoconvers = Nothing
	End Function
	
	'%Update. Este metodo se encarga de realizar actualizar de los datos correspondientes para la
	'%tabla "Noconvers". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Update() As Boolean
		Dim lrecNoconvers As eRemoteDB.Execute
		On Error GoTo Update_Err
		lrecNoconvers = New eRemoteDB.Execute
		With lrecNoconvers
			.StoredProcedure = "insNoconvers"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAreaWait", nAreaWait, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDevo", sDevo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDisc", sDisc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExpenses", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHealthexp", nHealthexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecNoconvers = Nothing
	End Function
	
	'%Delete. Este metodo se encarga de eliminar los registros  de los datos correspondientes para la
	'%tabla "Noconvers". Devolviendo verdadero o falso dependiendo de la existencia o no de los datos
	Public Function Delete() As Boolean
		Dim lrecNoconvers As eRemoteDB.Execute
		On Error GoTo Delete_err
		lrecNoconvers = New eRemoteDB.Execute
		With lrecNoconvers
			.StoredProcedure = "insNoconvers"
			.Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAreaWait", nAreaWait, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDevo", sDevo, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sDisc", sDisc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nExpenses", nExpenses, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nHealthexp", nHealthexp, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete = .Run(False)
		End With
Delete_err: 
		If Err.Number Then
			Delete = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecNoconvers = Nothing
	End Function
	
	'%insValMCA815: Validación de los campos que son ingresados en la popup de la pagina MCA815
	Public Function insValMCA815(ByVal sCodispl As String, ByVal sActions As String, ByVal nNo_convers As Integer, ByVal sDescript As String, ByVal nAreaWait As Integer, ByVal sDevo As String, ByVal sDisc As String, ByVal sStatregt As String) As String
		Dim lclsErrors As New eFunctions.Errors
		On Error GoTo insValMCA815_err
		
		'+ Si el campo causa no esta lleno, ninguna de los campos debe estar lleno
		If nNo_convers = eRemoteDB.Constants.intNull Or nNo_convers = 0 Then
			If sDescript <> String.Empty Or sDevo <> String.Empty Or sDisc <> String.Empty Or (nAreaWait <> eRemoteDB.Constants.intNull And nAreaWait <> 0) Or (sStatregt <> String.Empty And sStatregt <> "0") Then
				Call lclsErrors.ErrorMessage(sCodispl, 1084)
			End If
		End If
		
		If sActions = "Del" Then
			If Find_Noconvers(nNo_convers) Then
				Call lclsErrors.ErrorMessage(sCodispl, 55873)
			End If
		End If
		
		
		If sActions = "Add" Then
			'+ Si la acción es registrar el campo causa debe estar lleno
			If nNo_convers = eRemoteDB.Constants.intNull Or nNo_convers = 0 Then
				Call lclsErrors.ErrorMessage(sCodispl, 10872)
			Else
				'+ si la acción es registrar no debe existir en el sistema (Noconvers)
				If Find(nNo_convers) Then
					Call lclsErrors.ErrorMessage(sCodispl, 10004)
				End If
			End If
		End If
		
		'+ si el campo causa esta lleno, la descripcion tambien debe estar llena
		If nNo_convers <> eRemoteDB.Constants.intNull And nNo_convers <> 0 Then
			If sDescript = String.Empty Then
				Call lclsErrors.ErrorMessage(sCodispl, 10005)
			End If
			
			'+ Si el campo causa esta lleno, el estado debe estar lleno
			If (sStatregt = String.Empty Or sStatregt = "0") Then
				Call lclsErrors.ErrorMessage(sCodispl, 9089)
			End If
		End If
		
		If nAreaWait = eRemoteDB.Constants.intNull Or nAreaWait = 0 Then
			Call lclsErrors.ErrorMessage(sCodispl, 60485)
		End If
		
		insValMCA815 = lclsErrors.Confirm
		
insValMCA815_err: 
		If Err.Number Then
			insValMCA815 = "Noconvers.insValMCA815: " & Err.Description
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
	End Function
	
	'%insPostMCA815: Actualización de los datos ingresados en las causas pendientes
    Public Function insPostMCA815(ByVal sActions As String, ByVal nNo_convers As Integer, ByVal sDescript As String, ByVal nAreaWait As Integer, ByVal sDevo As String, ByVal sDisc As String, ByVal sStatregt As String, ByVal nUsercode As Integer, Optional ByVal nExpenses As Double = 0, Optional ByVal sRoutine As String = "", Optional ByVal nHealthexp As Double = 0) As Boolean
        With Me
            .nNo_convers = nNo_convers
            .sDescript = sDescript
            .nAreaWait = nAreaWait
            .sDevo = IIf(sDevo = "1", "1", "2")
            .sDisc = IIf(sDisc = "1", "1", "2")
            .sStatregt = sStatregt
            .nUsercode = nUsercode
            .nExpenses = nExpenses
            .nHealthexp = nHealthexp
            .sRoutine = sRoutine
            Select Case UCase(sActions)
                Case "ADD"
                    .nActions = 1
                    insPostMCA815 = .Add()
                Case "UPDATE"
                    .nActions = 2
                    insPostMCA815 = .Update()
                Case "DEL"
                    .nActions = 3
                    insPostMCA815 = .Delete()
            End Select
        End With
    End Function
	
	'%Find_WaitCode: Busca en certificat si existe alguna poliza certificado asociada a la causa
	'%               de no conversión
	Public Function Find_Noconvers(ByVal nNo_convers As Integer) As Boolean
		Dim lrecNoconvers As eRemoteDB.Execute
		On Error GoTo Find_Noconvers_Err
		lrecNoconvers = New eRemoteDB.Execute
		'Si nexist = 1 existen datos
		'Si nexist = 2 no existen datos
		
		With lrecNoconvers
			.StoredProcedure = "reaCertificat_Noconvers"
			With .Parameters
				.Add("nNo_convers", nNo_convers, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
				.Add("nExists", nExists, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			End With
			If .Run(False) Then
				Find_Noconvers = .Parameters("nExists").Value > 0
			End If
		End With
		
Find_Noconvers_Err: 
		If Err.Number Then
			Find_Noconvers = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecNoconvers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecNoconvers = Nothing
	End Function
	
	'%Class_Initialize: Se ejecuta cuando se instancia un objeto de la clase
	'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Initialize_Renamed()
		nNo_convers = eRemoteDB.Constants.intNull
		sDescript = String.Empty
		nAreaWait = eRemoteDB.Constants.intNull
		sDevo = String.Empty
		sDisc = String.Empty
		sStatregt = String.Empty
	End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
End Class






