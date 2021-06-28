Option Strict Off
Option Explicit On
Public Class Cond_cover_premium
	'%-------------------------------------------------------%'
	'% $Workfile:: Cond_cover_premium.cls                    $%'
	'% $Author:: JRengifo                                    $%'
	'% $Date:: 27/06/13 19.01                                $%'
	'% $Revision:: 1                                         $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla Cond_cover_premium tomada el 27/06/2013 10:06
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
    Public nPremium As Double
    Public nCapital_min As Double
    Public nCapital_max As Double
    Public nRate As Double
    Public sRoutine As string
    Public nId_table As Integer
    Public nId As Integer
	Public nCurrency As Integer ' NUMBER        22     5      0 Yes
	Public dNulldate As Date ' DATE           7              Yes
	Public dCompdate As Date ' DATE           7              No
	Public nUsercode As Integer ' NUMBER        22     5      0 No
	
	'% insValCA635: Esta rutina se encarga de realizar la validación de la página
    Public Function insValCA635(ByVal sAction As String, 
                                ByVal sCodispl As String, 
                                ByVal sCertype As String, 
                                ByVal nBranch As Integer, 
                                ByVal nProduct As Integer, 
                                ByVal nPolicy As Double, 
                                ByVal nGroup As Integer, 
                                Optional ByVal nCertif As Double = 0, 
                                Optional ByVal nModulec As Integer = 0, 
                                Optional ByVal nCover As Integer = 0, 
                                Optional ByVal nRole As Integer = 0, 
                                Optional ByVal dEffecdate As Date = #12:00:00 AM#, 
                                Optional ByVal nTypcond As Integer = 0, 
                                Optional ByVal nPremium As Double = 0, 
                                Optional ByVal nCapital_min As Double = 0, 
                                Optional ByVal nCapital_max As Double = 0, 
                                Optional ByVal nRate As Double = 0, 
                                Optional ByVal sRoutine As string = "", 
                                Optional ByVal nId_table As Integer = 0, 
                                Optional ByVal nCurrency As Integer = 0) As String

        Dim lobjErrors As eFunctions.Errors = New eFunctions.Errors
        Dim lclsPolicy As ePolicy.Policy = New ePolicy.Policy
        Dim lclsGroups As ePolicy.Groups
        Dim lclsModul_co_gp As Modul_co_gp

        On Error GoTo insValCA635_Err

        If sAction = "Add" Then
            If Find(sCertype, nBranch, nProduct, nPolicy, nGroup, nCertif, nModulec, nCover, nRole, dEffecdate, nId) Then
                lobjErrors.ErrorMessage(sCodispl, 198000)
            End If
        End If

        'Cobertura: Debe estar lleno
        If nCover = eRemoteDB.Constants.intNull Then
            lobjErrors.ErrorMessage(sCodispl, 4061)
        Else
            'Tipo de asegurado: Debe estar lleno
            If nRole = eRemoteDB.Constants.intNull Then
                lobjErrors.ErrorMessage(sCodispl, 10241)
            End If
        End If
        With lclsPolicy
            If .Find(sCertype, nBranch, nProduct, nPolicy) Then
                '+ Grupo del colectivo: Si se ha indicado en los datos de colectivo las coberturas "por grupos", 
                '                       la póliza debe tener grupos asociados
                If .sTyp_module = "3" Then
                    lclsGroups = New ePolicy.Groups
                    If lclsGroups.valGroupExist(sCertype, nBranch, nProduct, nPolicy, dEffecdate) = False Then
                        lobjErrors.ErrorMessage(sCodispl, 3887)
                    End If
                End If

                '+ Grupo del colectivo: Si en datos del colectivo se han indicado las coberturas por grupo, 
                '                       este campo debe estar lleno
                If .sTyp_module = "3" And nGroup = eRemoteDB.Constants.intNull Then
                    lobjErrors.ErrorMessage(sCodispl, 3308)
                End If

                '+ Módulo: Si se ha indicado uso de módulos a nivel de la póliza, debe estar lleno
                If nModulec = eRemoteDB.Constants.intNull Then
                    lclsModul_co_gp = New Modul_co_gp
                    If lclsModul_co_gp.valExistsModul_O(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nGroup) Then
                        lobjErrors.ErrorMessage(sCodispl, 1901)
                    End If
                End If

                '+ Moneda: Si el tipo de prima es "fija", o "Tasa según capital", debe estar lleno
                If nCurrency = eRemoteDB.Constants.intNull Then
                    Select Case nTypcond
                        Case 1, 2
                            lobjErrors.ErrorMessage(sCodispl, 750024)
                        Case Else
                            Exit Select
                    End Select
                End If

                Select Case nTypcond 
                    Case 1 'Prima fija

                    '+ Prima: Si el tipo de prima es "Prima fija", debe estar lleno
                        If nPremium = eRemoteDB.Constants.intNull Then
                            lobjErrors.ErrorMessage(sCodispl, 198001)
                        End If

                    Case 2 'Tasa según capital
                    
                    '+ Capital mínimo: Si el tipo de prima es "Tasa según capital", debe estar lleno
                        If nCapital_min = eRemoteDB.Constants.intNull Then
                            lobjErrors.ErrorMessage(sCodispl, 198002)
                        End If

                    '+ Capital máximo: Si el tipo de prima es "Tasa según capital", debe estar lleno
                        If nCapital_max = eRemoteDB.Constants.intNull Then
                            lobjErrors.ErrorMessage(sCodispl, 198003)
                        End If

                    '+ Capital máximo: Si está lleno, debe ser mayor que el Capital mínimo
                        If nCapital_min <> eRemoteDB.Constants.intNull AndAlso
                           nCapital_max <> eRemoteDB.Constants.intNull Andalso 
                           nCapital_min > nCapital_max Then
                            lobjErrors.ErrorMessage(sCodispl, 198004)
                        End If

                    '+ Tasa: Si el tipo de prima es "Tasa según capital", debe estar lleno
                        If nRate = eRemoteDB.Constants.intNull Then
                            lobjErrors.ErrorMessage(sCodispl, 198005)
                        End If

                    Case 3 'Rutina

                        '+ Rutina: Si el tipo de prima es "Rutina", debe estar lleno
                        If sRoutine = eRemoteDB.Constants.strNull Then
                            lobjErrors.ErrorMessage(sCodispl, 198006)
                        End If
                    
                    Case 4 'Tabla lógica de tarifas
                    
                    '+ Tabla lógica de tarifas: Si el tipo de prima es "Tabla lógica de tarifas", debe estar lleno
                        If nId_table = eRemoteDB.Constants.intNull Then
                            lobjErrors.ErrorMessage(sCodispl, 198007)
                        End If

                    Case Else
                        Exit Select
                End Select

            End If
        End With

        insValCA635 = lobjErrors.Confirm

insValCA635_Err:
        If Err.Number Then
            insValCA635 = "insValCA635: " & Err.Description
        End If
        lobjErrors = Nothing
        lclsGroups = Nothing
        lclsPolicy = Nothing
        lclsModul_co_gp = Nothing
        On Error GoTo 0
    End Function
	
	'% insValCA635Upd: Esta rutina se encarga de realizar la validación de la página
    Public Function insValCA635Upd(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer,
                                   ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer,
                                   Optional ByVal nCertif As Double = 0, Optional ByVal nModulec As Integer = 0,
                                   Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCopy As String = "2") As String
        Dim lobjErrors As eFunctions.Errors = New eFunctions.Errors
        Dim lobjPolicy As ePolicy.Policy = New ePolicy.Policy
        Dim lclsGroups As ePolicy.Groups
        Dim lclsModul_co_gp As Modul_co_gp

        On Error GoTo insValCA635Upd_Err
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

                        '+ Si se definieron modulos para la póliza, debe estar lleno
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
                lobjErrors.ErrorMessage(sCodispl, 198008)
            End If
        End If

        insValCA635Upd = lobjErrors.Confirm

insValCA635Upd_Err:
        If Err.Number Then
            insValCA635Upd = "insValCA635Upd: " & Err.Description
        End If
        lobjErrors = Nothing
        lclsGroups = Nothing
        lobjPolicy = Nothing
        lclsModul_co_gp = Nothing
        On Error GoTo 0
    End Function
	
	'%Add: Crea un registro en la tabla
	Public Function Add() As Boolean
        Dim lreccreaCond_cover_premium As eRemoteDB.Execute = New eRemoteDB.Execute
		
		On Error GoTo Add_err
		
		With lreccreaCond_cover_premium
			.StoredProcedure = "creACond_cover_premium"
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
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_min", nCapital_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_max", nCapital_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			Add = .Run(False)
		End With
		
Add_err: 
		If Err.Number Then
			Add = False
		End If
		On Error GoTo 0
		lreccreaCond_cover_premium = Nothing
		
	End Function
	
	'% insPostCA635: Esta función se encarga de actualizar los datos introducidos en la página
    Public Function insPostCA635(ByVal sAction As String, ByVal sCertype As String, ByVal nBranch As Integer,
                                 ByVal nProduct As Integer, ByVal nPolicy As Double, Optional ByVal nGroup As Integer = 0,
                                 Optional ByVal nCertif As Double = 0, Optional ByVal nModulec As Integer = 0, Optional ByVal nCover As Integer = 0,
                                 Optional ByVal nRole As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nTypcond As Integer = 0,
                                    Optional ByVal nPremium As Double = 0,
                                    Optional ByVal nCapital_min As Double = 0,
                                    Optional ByVal nCapital_max As Double = 0,
                                    Optional ByVal nRate As Double = 0,
                                    Optional ByVal sRoutine As String = "",
                                    Optional ByVal nId_table As Integer = 0,
                                 Optional ByVal nCurrency As Integer = 0,
                                 Optional ByVal nUsercode As Integer = 0,
                                 Optional ByVal nId As Integer = 0) As Boolean

        Dim lclsPolicyWin As ePolicy.Policy_Win = New ePolicy.Policy_Win
        Dim lcolCond_cover_premiums As ePolicy.Cond_cover_premiums = New ePolicy.Cond_cover_premiums

        On Error GoTo insPostCA635_err

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
            .nPremium = nPremium
            .nCapital_min = nCapital_min
            .nCapital_max = nCapital_max
            .nRate = nRate
            .sRoutine = sRoutine
            .nId_table = nId_table
            .nCurrency = nCurrency
            .nUsercode = nUsercode
            .nId = nId
        End With

        insPostCA635 = True

        Select Case sAction
            Case "Add"
                insPostCA635 = Me.Add
            Case "Update"
                insPostCA635 = Me.Update
            Case "Del"
                insPostCA635 = Me.Del
            Case Else
                Exit Select
        End Select

        If insPostCA635 Then
            Call lcolCond_cover_premiums.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, nGroup, nModulec, dEffecdate)
            If lcolCond_cover_premiums.Count = 0 Then
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA635", "1")
            Else
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA635", "2")
                Call lclsPolicyWin.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, "CA014A", CStr(3))
            End If
        End If

insPostCA635_err:
        If Err.Number Then
            insPostCA635 = False
        End If
        On Error GoTo 0
        lclsPolicyWin = Nothing
        lcolCond_cover_premiums = Nothing

    End Function

    '% Del: Este método se encarga de eliminar registros en la tabla "Commission". Devolviendo verdadero o
    '% falso dependiendo de si el Stored procedure se ejecutó correctamente.
    Public Function Del() As Boolean
        Dim lrecdelCond_cover_premium As eRemoteDB.Execute = New eRemoteDB.Execute

        On Error GoTo Del_err

        With lrecdelCond_cover_premium
            .StoredProcedure = "delCond_cover_premium"
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
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Del = .Run(False)
        End With
        lrecdelCond_cover_premium = Nothing
Del_err:
        If Err.Number Then
            Del = False
        End If
        On Error GoTo 0
        lrecdelCond_cover_premium = Nothing

    End Function

    '% Update: Se actualizan los registros ingresados en la página
    Public Function Update() As Boolean
        Dim lrecupdCond_cover_premium As eRemoteDB.Execute = New eRemoteDB.Execute

        On Error GoTo Update_Err

        With lrecupdCond_cover_premium
            .StoredProcedure = "insUpdCond_cover_premium"
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
            .Parameters.Add("nTypcond", nTypcond, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPremium", nPremium, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_min", nCapital_min, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCapital_max", nCapital_max, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 4, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutine", sRoutine, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId_table", nId_table, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nId", nId, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        lrecupdCond_cover_premium = Nothing

    End Function

    '% Find: Verifica si la póliza tiene comisiones asociadas
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nCertif As Double, ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer, ByVal dEffecdate As Date, ByVal nId As Integer) As Boolean
        Dim lclsCond_cover_premium As eRemoteDB.Execute = New eRemoteDB.Execute

        On Error GoTo Find_Err

        With lclsCond_cover_premium
            .StoredProcedure = "reaCond_cover_premium"
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
            .Parameters.Add("nId", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                Find = IIf(.Parameters("nExists").Value > 0, True, False)
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        On Error GoTo 0
        lclsCond_cover_premium = Nothing
    End Function

    '% FindGroupCover: Verifica si la póliza tiene comisiones asociadas a un grupo cuando carga
    ' la página
    Public Function FindGroupCover(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double) As Boolean
        Dim lclsCond_cover_premium As eRemoteDB.Execute = New eRemoteDB.Execute

        On Error GoTo FindGroupCover_Err

        With lclsCond_cover_premium
            .StoredProcedure = "REAEXISTCond_cover_premium"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
        lclsCond_cover_premium = Nothing
    End Function

    '% Delete_All: Elimina todas las condiciones de capital asociados a la póliza
    Public Function Delete_All(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lclsRemote As eRemoteDB.Execute

        On Error GoTo Delete_All_Err

        lclsRemote = New eRemoteDB.Execute

        With lclsRemote
            .StoredProcedure = "delCond_cover_premium_All"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
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
        lclsRemote = Nothing
    End Function

    '% insPostCA635: Esta función se encarga de actualizar los datos introducidos en la página
    Public Function insPostCA635Copy(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                     ByVal nPolicy As Double, ByVal nCertif As Double, Optional ByVal nGroup As Integer = 0,
                                     Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean

        Dim lblnUpdCover As Boolean
        Dim lrecInsPostCA635Copy As eRemoteDB.Execute = New eRemoteDB.Execute

        On Error GoTo insPostCA635Copy_Err
        With lrecInsPostCA635Copy
            .StoredProcedure = "InsCopyCA635"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nGroup", nGroup, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUpdPremium", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            insPostCA635Copy = .Parameters("nUpdPremium").Value = 1

        End With

insPostCA635Copy_Err:
        If Err.Number Then
            insPostCA635Copy = False
        End If
        lrecInsPostCA635Copy = Nothing
        On Error GoTo 0

    End Function

    '% Find: Verifica si la póliza tiene comisiones asociadas
    Public Function FindOthersGroup(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer,
                                    ByVal nPolicy As Double, ByVal nGroup As Integer, ByVal nCertif As Double,
                                    ByVal nModulec As Integer, ByVal nCover As Integer, ByVal nRole As Integer,
                                    ByVal dEffecdate As Date) As Boolean
        Dim lclsCond_cover_premium As eRemoteDB.Execute

        On Error GoTo Find_Err

        lclsCond_cover_premium = New eRemoteDB.Execute

        With lclsCond_cover_premium
            .StoredProcedure = "REACond_cover_premium_DIF"
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
        lclsCond_cover_premium = Nothing
    End Function

End Class