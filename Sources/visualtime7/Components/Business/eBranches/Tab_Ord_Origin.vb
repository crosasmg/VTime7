Option Strict Off
Option Explicit On
Public Class Tab_Ord_Origin
	'%-------------------------------------------------------%'
	'% $Workfile:: Tab_Ord_Origin.cls                       $%'
	'% $Author:: Clobos                                     $%'
	'% $Date:: 7/02/06 11:11                                $%'
	'% $Revision:: 2                                        $%'
	'%-------------------------------------------------------%'
	
	'+ Definición de la tabla Tab_Ord_Origin tomada el 13/01/2003.
	'+ Column_Name                       Type         Length  Prec     Scale  Nullable
	'------------------------------    --------------- -   -------- ------- --------
	Public nOrigin As Integer ' NUMBER        22     5      0         No
	Public nOrder As Integer ' NUMBER        22     5      0         No
	Public nUsercode As Integer ' NUMBER               5
	Public sDescript As String
	Public sPrimary As String
	Public nPerc_collect As Integer
    Public sSell_cost As String
    Public dExpirdat As Date
    Public nOrigen_dep As Integer


	
	'-[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
	Public nBranch As Integer
	Public nProduct As Integer
	
	'%Add: Esta función agrega registros a la tabla Tab_Ord_Origin.
	Public Function Add() As Boolean
		Add = InsUpdTab_Ord_Origin(1)
	End Function
	
	'%Update: Esta función actualiza registros en la tabla Tab_Ord_Origin.
	Public Function Update() As Boolean
		Update = InsUpdTab_Ord_Origin(2)
	End Function
	
	'%Delete: Esta función elimina registros de la tabla Tab_Ord_Origin.
	Public Function Delete() As Boolean
		Delete = InsUpdTab_Ord_Origin(3)
	End Function
	
	'% InsUpdTab_Ord_Origin: Actualiza la informacion de la tabla Tab_Ord_Origin.
	Private Function InsUpdTab_Ord_Origin(ByVal nAction As Integer) As Boolean
		Dim lrecInsUpdTab_Ord_Origin As eRemoteDB.Execute
		
		On Error GoTo InsUpdTab_Ord_Origin_Err
		
		lrecInsUpdTab_Ord_Origin = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'insupdtab_ord_origin'
		'**+ The Information was read on  04/09/2003
		
		'+ Definición de parámetros para stored procedure 'insupdtab_ord_origin'
		'+ Información leída el: 04/09/2003
		
		'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
		
		With lrecInsUpdTab_Ord_Origin
			.StoredProcedure = "insUpdTab_Ord_Origin"
			
			.Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 0, 0, 38, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPrimary", sPrimary, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPerc_COllect", nPerc_collect, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sSell_cost", sSell_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dExpirdat", dExpirdat, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigen_dep", nOrigen_dep, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 0, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

			InsUpdTab_Ord_Origin = .Run(False)
		End With
		
		'UPGRADE_NOTE: Object lrecInsUpdTab_Ord_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTab_Ord_Origin = Nothing
		
InsUpdTab_Ord_Origin_Err: 
		If Err.Number Then
			InsUpdTab_Ord_Origin = False
		End If
		
		'UPGRADE_NOTE: Object lrecInsUpdTab_Ord_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecInsUpdTab_Ord_Origin = Nothing
		
		On Error GoTo 0
	End Function
	
	'%IsExist: Este método retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%         tabla "Tab_Ord_Origin"
	Public Function IsExist(ByVal nOrder As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecReaTab_Ord_Origin As eRemoteDB.Execute
		
		On Error GoTo IsExist_Err
		
		lrecReaTab_Ord_Origin = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'reaexistorigin'
		'**+ The Information was read on  04/09/2003
		
		'+ Definición de parámetros para stored procedure 'reaexistorigin'
		'+ Información leída el: 04/09/2003
		
		'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
		
		With lrecReaTab_Ord_Origin
			.StoredProcedure = "reaExistTab_Ord_Origin"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrder", nOrder, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			
			IsExist = .Parameters("nCount").Value > 0
		End With
		
IsExist_Err: 
		If Err.Number Then
			IsExist = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaTab_Ord_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_Ord_Origin = Nothing
		
		On Error GoTo 0
	End Function
	
	'%IsExistOrigin: Este método retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
	'%               tabla "Tab_Ord_Origin".
	Public Function IsExistOrigin(ByVal nOrigin As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
		Dim lrecReaTab_Ord_Origin As eRemoteDB.Execute
		
		On Error GoTo IsExistOrigin_Err
		
		lrecReaTab_Ord_Origin = New eRemoteDB.Execute
		
		'**+ Definition of parameters for stored procedure 'reaexistorigin'
		'**+ The Information was read on  04/09/2003
		
		'+ Definición de parámetros para stored procedure 'reaexistorigin'
		'+ Información leída el: 04/09/2003
		
		'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
		
		With lrecReaTab_Ord_Origin
			.StoredProcedure = "reaExistOrigin"
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			.Run(False)
			
			IsExistOrigin = .Parameters("nCount").Value > 0
		End With
		
IsExistOrigin_Err: 
		If Err.Number Then
			IsExistOrigin = False
		End If
		
		'UPGRADE_NOTE: Object lrecReaTab_Ord_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecReaTab_Ord_Origin = Nothing
		
		On Error GoTo 0
	End Function
	
	'% InsValMVI7002: Esta función se encarga de validar los datos introducidos en la venta MVI7002
	'% Tabla de orden de uso de las cuentas origen para pagar cargos (APV).
	'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
	Public Function InsValMVI7002(ByVal sCodispl As String, ByVal sAction As String, ByVal nOrigin As Integer, ByVal nOrder As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal sPrimary As String, ByVal nPerc_collect As Integer) As String
		Dim lclsErrors As eFunctions.Errors
		
		On Error GoTo InsValMVI7002_Err
		
		lclsErrors = New eFunctions.Errors
		
		With lclsErrors
			
			'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
			
			'+ Validaciones del campo Origen.
			
			If nOrigin = eRemoteDB.Constants.intNull Or nOrigin = 0 Then
				.ErrorMessage(sCodispl, 70090)
			End If
			
			'+ Validaciones del campo Orden.
			
			If nOrder = eRemoteDB.Constants.intNull Or nOrder = 0 Then
				.ErrorMessage(sCodispl, 70135)
			Else
				If sAction = "Add" Then
					If IsExist(nOrder, nBranch, nProduct) Then
						.ErrorMessage(sCodispl, 70136)
					End If
				End If
			End If
			
			If sAction = "Add" Then
				If nOrigin <> 0 And nOrigin <> eRemoteDB.Constants.intNull Then
					If IsExistOrigin(nOrigin, nBranch, nProduct) Then
						.ErrorMessage(sCodispl, 70143)
					End If
				End If
			End If
			
			If sPrimary = "1" Then
				If nPerc_collect <> 0 And nPerc_collect <> eRemoteDB.Constants.intNull Then
					.ErrorMessage(sCodispl, 200006)
				End If
				
				If IsExistOriginPrimary(nOrigin, nBranch, nProduct) Then
					.ErrorMessage(sCodispl, 200005)
				End If
			End If
			
			If nPerc_collect > 100 Then
				.ErrorMessage(sCodispl, 11239)
			End If
			
			
			InsValMVI7002 = lclsErrors.Confirm
		End With
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
InsValMVI7002_Err: 
		If Err.Number Then
			InsValMVI7002 = "InsValMVI7002: " & Err.Description
		End If
		
		'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lclsErrors = Nothing
		
		On Error GoTo 0
	End Function
	
	'% InsPostMVI7002Upd: Esta función se encarga de crear/actualizar los registros
	'%                    correspondientes en la tabla TAB_ORD_ORIGIN.
	'+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003
	Public Function InsPostMVI7002Upd(ByVal sAction As String, ByVal nOrigin As Integer, ByVal nOrder As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nUsercode As Integer, ByVal sPrimary As String, ByVal nPerc_collect As Integer, BYVAL sSell_cost As String, BYVAL dExpirdat As Date,BYVAL nOrigen_dep As Integer) As Boolean
        On Error GoTo InsPostMVI7002Upd_Err

        '+[APV2]  HAD 1021. Tabla de orden de aplicacion de las cuentas origen. DBLANCO 05-09-2003

        With Me
            .nOrigin = nOrigin
            .nOrder = nOrder
            .nBranch = nBranch
            .nProduct = nProduct
            .nUsercode = nUsercode
            .sPrimary = sPrimary
            .nPerc_collect = nPerc_collect
            .sSell_cost = sSell_cost
            .dExpirdat = dExpirdat
            .nOrigen_dep = nOrigen_dep

            InsPostMVI7002Upd = True

            Select Case sAction

                '+ Si la opción seleccionada es Registrar.

                Case "Add"
                    InsPostMVI7002Upd = .Add()

                    '+ Si la opción seleccionada es Modificar.

                Case "Update"
                    InsPostMVI7002Upd = .Update()

                    '+ Si la opción seleccionada es Eliminar.

                Case "Del"
                    InsPostMVI7002Upd = .Delete()
            End Select
        End With

InsPostMVI7002Upd_Err:
        If Err.Number Then
            InsPostMVI7002Upd = False
        End If

        On Error GoTo 0
    End Function


    '% InsValMVI7002_K: Esta función se encarga de validar los datos introducidos en la venta MVI7002
    '% Tabla de orden de uso de las cuentas origen para pagar cargos (APV).
    Public Function InsValMVI7002_K(ByVal sCodispl As String, ByVal nBranch As Integer, ByVal nProduct As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValMVI7002_K_Err

        lclsErrors = New eFunctions.Errors

        With lclsErrors

            If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
                .ErrorMessage(sCodispl, 1022)
            End If

            If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
                .ErrorMessage(sCodispl, 1011)
            End If

            InsValMVI7002_K = lclsErrors.Confirm
        End With

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

InsValMVI7002_K_Err:
        If Err.Number Then
            InsValMVI7002_K = "InsValMVI7002_K: " & Err.Description
        End If

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

        On Error GoTo 0
    End Function

    '%IsExistOriginPrimary: Este método retorna VERDADERO o FALSO dependiendo de la existencia o no de
    '%                      una cuenta básica
    Public Function IsExistOriginPrimary(ByVal nOrigin As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        Dim lrecReaTab_Ord_Origin As eRemoteDB.Execute

        On Error GoTo IsExistOriginPrimary_Err

        lrecReaTab_Ord_Origin = New eRemoteDB.Execute

        With lrecReaTab_Ord_Origin
            .StoredProcedure = "REAEXISTORIGINPRIMARY"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)

            IsExistOriginPrimary = .Parameters("nCount").Value > 0
        End With

IsExistOriginPrimary_Err:
        If Err.Number Then
            IsExistOriginPrimary = False
        End If
        'UPGRADE_NOTE: Object lrecReaTab_Ord_Origin may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecReaTab_Ord_Origin = Nothing
        On Error GoTo 0
    End Function
End Class






