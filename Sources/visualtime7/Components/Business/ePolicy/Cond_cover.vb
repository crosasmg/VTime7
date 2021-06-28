Option Strict Off
Option Explicit On
Public Class Cond_cover
	'%-------------------------------------------------------%'
	'% $Workfile:: Cond_cover.cls                           $%'
	'% $Author:: Nvaplat41                                  $%'
	'% $Date:: 9/10/03 19.01                                $%'
	'% $Revision:: 40                                       $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla COND_COVER tomada el 29/10/2001 10:06
	'+ Column_Name                                   Type      Length  Prec  Scale Nullable
	'----------------------------- --------------- - -------- ------- ----- ------ --------
	Public sCertype As String ' CHAR           1              No
	Public nBranch As Integer ' NUMBER        22     5      0 No
	Public nProduct As Integer ' NUMBER        22     5      0 No
	Public nPolicy As Double ' NUMBER        22    10      0 No
	Public nGroup As Integer ' NUMBER        22     5      0 No
	Public nCertif As Double ' NUMBER        22    10      0 No
	Public nModulec As Integer ' NUMBER        22     5      0 No
	Public nCover As Integer ' NUMBER        22     5      0 No
	Public nRole As Integer ' NUMBER        22     5      0 No
	Public dEffecdate As Date ' DATE           7              No
	Public nTypcond As Integer ' NUMBER        22     5      0 No
	Public nAmount As Double ' NUMBER        22    12      0 Yes
	Public nPercent As Double ' NUMBER        22     5      2 Yes
	Public nRent As Integer ' NUMBER        22     5      0 Yes
	Public nCurrency As Integer ' NUMBER        22     5      0 Yes
	Public dNulldate As Date ' DATE           7              Yes
	Public dCompdate As Date ' DATE           7              No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
    Public nMonthI As Integer
    Public nMonthE As Integer
    Public nID As Integer
	'% insValCA639: Esta rutina se encarga de realizar la validación de la página
    Public Function insValCA639(ByVal sAction As String, ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, Optional ByVal nCertif As Double = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nRole As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nTypcond As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nPercent As Double = 0, Optional ByVal nRent As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional nID As Integer = 0, Optional nMonthI As Integer = 0, Optional nMonthE As Integer = 0) As String
        Dim lobjErrors As eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsGroups As ePolicy.Groups
        Dim lclsModul_co_gp As Modul_co_gp

        On Error GoTo insValCA639_Err

        lobjErrors = New eFunctions.Errors

        If sAction = "Add" Then
            If Find(sCertype, nBranch, nProduct, nPolicy, nGroup, nCertif, nModulec, nCover, nRole, dEffecdate, nMonthI, nMonthE) Then
                lobjErrors.ErrorMessage(sCodispl, 55902)
            End If
        End If

        If nCover = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 4061)
        Else
            If nRole = eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 10241)
            End If
        End If

        lclsPolicy = New ePolicy.Policy
        With lclsPolicy
            If .Find(sCertype, nBranch, nProduct, nPolicy) Then

                '+ Se indicaron grupos colectivos
                If .sTyp_module = "3" Then
                    lclsGroups = New ePolicy.Groups
                    If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) = False Then
                        lobjErrors.ErrorMessage(sCodispl, 3887)
                    End If
                End If

                '+ validaciones de modulos por modulos y numero de grupo
                If .sTyp_module = "3" And nGroup = eRemoteDB.Constants.intNull Then
                    lobjErrors.ErrorMessage(sCodispl, 3308)
                End If

                '+ validaciones de modulos por modulos y numero de grupo
                If nModulec = eRemoteDB.Constants.intNull Then
                    lclsModul_co_gp = New Modul_co_gp
                    If lclsModul_co_gp.valExistsModul_O(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup) Then

                        '+ Si se definieron grupos para la póliza, debe estar lleno
                        lobjErrors.ErrorMessage(sCodispl, 1901)
                    End If
                End If

                '+ verifica que los campo esten llenos dependiendo del valor de "tipo de capital (nTyp_cond)".
                '+ "monto fijo" nulo y "tipo de capital" en "fijo" o "fijo mas variable"
                If nAmount = eRemoteDB.Constants.intNull And (nTypcond = 1 Or nTypcond = 3) Then
                    lobjErrors.ErrorMessage(sCodispl, 11326)
                End If

                '+ "moneda" requerida si tipo de capital es 1-"fijo" o  3-"fijo mas variable"
                If nCurrency = eRemoteDB.Constants.intNull Then
                    Select Case nTypcond
                        Case 1, 3
                            lobjErrors.ErrorMessage(sCodispl, 750024)
                    End Select
                End If

                '+ "veces renta" nula y "tipo de capital" en "variable" o "fijo mas variable"
                If nRent = eRemoteDB.Constants.intNull Then
                    Select Case nTypcond
                        Case 2, 3
                            lobjErrors.ErrorMessage(sCodispl, 55619)
                    End Select
                End If

                '+ "% Cobertura Principal" nula y "tipo de capital" en "% cobertura princial"
                If nPercent = eRemoteDB.Constants.intNull And nTypcond = 4 Then
                    lobjErrors.ErrorMessage(sCodispl, 55620)
                End If
            End If
        End With

        insValCA639 = lobjErrors.Confirm

insValCA639_Err:
        If Err.Number Then
            insValCA639 = "insValCA639: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsModul_co_gp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsModul_co_gp = Nothing
        On Error GoTo 0
    End Function
	
	'% insValCA639Upd: Esta rutina se encarga de realizar la validación de la página
    Public Function insValCA639Upd(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer,
                                   ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer,
                                   Optional ByVal nCertif As Double = 0, Optional ByVal nModulec As Integer = 0,
                                   Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCopy As String = "2") As String
        Dim lobjErrors As eFunctions.Errors
        Dim lobjPolicy As ePolicy.Policy
        Dim lclsGroups As ePolicy.Groups
        Dim lclsModul_co_gp As Modul_co_gp

        On Error GoTo insValCA639Upd_Err

        lobjErrors = New eFunctions.Errors
        lobjPolicy = New ePolicy.Policy
        With lobjPolicy
            If .Find(sCertype, nBranch, nProduct, nPolicy) Then

                '+ Se indicaron grupos colectivos
                If .sTyp_module = "3" Then
                    lclsGroups = New ePolicy.Groups
                    If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) = False Then
                        lobjErrors.ErrorMessage(sCodispl, 3887)
                    End If
                End If

                '+ Validaciones de modulos por modulos y numero de grupo
                If .sTyp_module = "3" And nGroup = eRemoteDB.Constants.intNull Then
                    lobjErrors.ErrorMessage(sCodispl, 3308)
                End If

                '+ Validaciones de modulos por modulos y numero de grupo
                If nModulec = eRemoteDB.Constants.intNull Then
                    lclsModul_co_gp = New Modul_co_gp
                    If lclsModul_co_gp.valExistsModul_O(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup) Then

                        '+ Si se definieron grupos para la póliza, debe estar lleno
                        lobjErrors.ErrorMessage(sCodispl, 1901)
                    End If
                End If
            End If
        End With

        '+ Esta validacion debe ir solo en la poliza matriz
        If sCopy = "1" And nCertif = 0 Then
            If FindOthersGroup(sCertype, nBranch, nProduct, nPolicy, nGroup, nCertif, 0, 0, 0, dEffecdate) Then
                lobjErrors.ErrorMessage(sCodispl, 14)
            Else
                lobjErrors.ErrorMessage(sCodispl, 13)
            End If
        End If

        insValCA639Upd = lobjErrors.Confirm

insValCA639Upd_Err:
        If Err.Number Then
            insValCA639Upd = "insValCA639Upd: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
        'UPGRADE_NOTE: Object lclsGroups may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsGroups = Nothing
        'UPGRADE_NOTE: Object lobjPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjPolicy = Nothing
        'UPGRADE_NOTE: Object lclsModul_co_gp may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsModul_co_gp = Nothing
        On Error GoTo 0
    End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
		Dim lreccreaCond_cover As eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		lreccreaCond_cover = New eRemoteDB.Execute
		
		With lreccreaCond_cover
			.StoredProcedure = "creAcond_cover"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypcond", nTypcond, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRent", nRent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nID, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonthI", nMonthI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonthE", nMonthE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lreccreaCond_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lreccreaCond_cover = Nothing
		
	End Function
	
	'% insPostCA639: Esta función se encarga de actualizar los datos introducidos en la página
    Public Function insPostCA639(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByVal nGroup As Integer = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0, Optional ByVal nRole As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nTypcond As Integer = 0, Optional ByVal nAmount As Double = 0, Optional ByVal nPercent As Double = 0, Optional ByVal nRent As Integer = 0, Optional ByVal nCurrency As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nid As Integer = 0, Optional ByVal nMonthI As Integer = 0, Optional ByVal nMonthE As Integer = 0) As Boolean
        Dim lclsPolicyWin As ePolicy.Policy_Win
        Dim lcolCond_covers As ePolicy.Cond_covers

        On Error GoTo insPostCA639_err

        lclsPolicyWin = New ePolicy.Policy_Win
        lcolCond_covers = New ePolicy.Cond_covers

        With Me
            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nGroup = nGroup
            .nCertif = nCertif
            .nModulec = nModulec
            .nCover = nCover
            .nRole = nRole
            .dEffecdate = dEffecdate
            .nTypcond = nTypcond
            .nAmount = nAmount
            .nPercent = nPercent
            .nRent = nRent
            .nCurrency = nCurrency
            .nUsercode = nUsercode
            .nID = nID
            .nMonthI = nMonthI
            .nMonthE = nMonthE

        End With

        insPostCA639 = True

        Select Case sAction
            Case "Add"
                insPostCA639 = Me.Add
            Case "Update"
                insPostCA639 = Me.Update
            Case "Del"
                insPostCA639 = Me.Del
        End Select

        If insPostCA639 Then
            Call lcolCond_covers.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, nModulec, dEffecdate)
            If lcolCond_covers.Count = 0 Then
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA639", "1")
            Else
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA639", "2")
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014A", CStr(3))
            End If
        End If

insPostCA639_err:
        If Err.Number Then
            insPostCA639 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsPolicyWin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicyWin = Nothing
        'UPGRADE_NOTE: Object lcolCond_covers may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lcolCond_covers = Nothing

    End Function
	
	'% Del: Este método se encarga de eliminar registros en la tabla "Commission". Devolviendo verdadero o
	'% falso dependiendo de si el Stored procedure se ejecutó correctamente.
	Public Function Del() As Boolean
		Dim lrecdelCond_cover As eRemoteDB.Execute
		
		On Error GoTo Del_err
		
		lrecdelCond_cover = New eRemoteDB.Execute
		
		With lrecdelCond_cover
			.StoredProcedure = "delCond_cover"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nID, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Del = .Run(False)
		End With
		'UPGRADE_NOTE: Object lrecdelCond_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCond_cover = Nothing
Del_err: 
		If Err.Number Then
			Del = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecdelCond_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecdelCond_cover = Nothing
		
	End Function
	
	'% Update: Se actualizan los registros ingresados en la página
	Public Function Update() As Boolean
		Dim lrecupdCond_cover As eRemoteDB.Execute
		
		On Error GoTo Update_Err
		
		lrecupdCond_cover = New eRemoteDB.Execute
		
		With lrecupdCond_cover
			.StoredProcedure = "insUpdcond_cover"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nTypcond", nTypcond, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nRent", nRent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nID, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonthI", nID, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonthE", nID, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Update = .Run(False)
		End With
		
Update_Err: 
		If Err.Number Then
			Update = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecupdCond_cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecupdCond_cover = Nothing
		
	End Function
	
	'% Find: Verifica si la póliza tiene comisiones asociadas
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nMonthI As Integer, ByVal nMonthE As Integer) As Boolean
        Dim lclsCond_Cover As eRemoteDB.Execute

        On Error GoTo Find_Err

        lclsCond_Cover = New eRemoteDB.Execute

        With lclsCond_Cover
            .StoredProcedure = "reaCond_cover"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonthI", nMonthI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMonthE", nMonthE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Find = IIf(.Parameters("nExists").Value > 0, True, False)
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCond_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCond_Cover = Nothing
    End Function
	
	'% FindGroupCover: Verifica si la póliza tiene comisiones asociadas a un grupo cuando carga
	' la página
	Public Function FindGroupCover(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
		Dim lclsCond_Cover As eRemoteDB.Execute
		
		On Error GoTo FindGroupCover_Err
		
		lclsCond_Cover = New eRemoteDB.Execute
		
		With lclsCond_Cover
			.StoredProcedure = "REAEXISTCOND_COVER"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			If .Run(False) Then
				nGroup = .Parameters("nGroup").Value
				FindGroupCover = True
			End If
		End With
		
FindGroupCover_Err: 
		If Err.Number Then
			FindGroupCover = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsCond_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsCond_Cover = Nothing
	End Function
	
	'% Delete_All: Elimina todas las condiciones de capital asociados a la póliza
	Public Function Delete_All(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
		Dim lclsRemote As eRemoteDB.Execute
		
		On Error GoTo Delete_All_Err
		
		lclsRemote = New eRemoteDB.Execute
		
		With lclsRemote
			.StoredProcedure = "delCond_cover_All"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Delete_All = .Run(False)
		End With
		
Delete_All_Err: 
		If Err.Number Then
			Delete_All = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lclsRemote may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsRemote = Nothing
	End Function





    '% insPostCA639: Esta función se encarga de actualizar los datos introducidos en la página
    Public Function insPostCA639Copy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                     ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal nGroup As Integer = 0,
                                     Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean

        Dim lblnUpdCover As Boolean
        Dim lrecInsPostCA639Copy As eRemoteDB.Execute

        On Error GoTo insPostCA639Copy_Err

        lrecInsPostCA639Copy = New eRemoteDB.Execute
        With lrecInsPostCA639Copy
            .StoredProcedure = "InsCopyCA639"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpdCapital", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            insPostCA639Copy = .Parameters("nUpdCapital").Value = 1

        End With

insPostCA639Copy_Err:
        If Err.Number Then
            insPostCA639Copy = False
        End If
        'UPGRADE_NOTE: Object lrecInsPostCA014Copy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsPostCA639Copy = Nothing
        On Error GoTo 0

    End Function

    '% Find: Verifica si la póliza tiene comisiones asociadas
    Public Function FindOthersGroup(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                    ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nCertif As Double,
                                    ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer,
                                    ByVal dEffecdate As Date) As Boolean
        Dim lclsCond_Cover As eRemoteDB.Execute

        On Error GoTo Find_Err

        lclsCond_Cover = New eRemoteDB.Execute

        With lclsCond_Cover
            .StoredProcedure = "REACOND_COVER_DIF"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nModulec", nModulec, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nCover", nCover, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '.Parameters.Add("nRole", nRole, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExists", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                FindOthersGroup = IIf(.Parameters("nExists").Value > 0, True, False)
            End If
        End With

Find_Err:
        If Err.Number Then
            FindOthersGroup = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsCond_Cover may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCond_Cover = Nothing
    End Function

End Class






