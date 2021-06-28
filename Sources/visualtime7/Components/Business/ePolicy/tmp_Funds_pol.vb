Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("tmp_Funds_Pol_NET.tmp_Funds_Pol")> Public Class tmp_Funds_Pol
	'%-------------------------------------------------------%'
	'% $Workfile:: tmp_Funds_Pol.cls                            $%'
	'% $Author:: Gazuaje                                    $%'
	'% $Date:: 3/07/06 7:39p                                $%'
	'% $Revision:: 21                                       $%'
	'%-------------------------------------------------------%'
	
	'**-Objective: Type or Record. Sole values:     1-  Proposal     2 - Policy     3 - Quotation
	'-Objetivo: Tipo de registro. Valores únicos:    1 - Solicitud    2 - Póliza    3 - Cotización
	Public sCertype As String
	
	'**-Objective: Code of the Line of Business. The possible values as per table 10.
	'-Objetivo: Código del ramo comercial. Valores posibles según tabla 10.
	Public nBranch As Integer
	
	'**-Objective: Code of the product.
	'-Objetivo: Código del producto.
	Public nProduct As Integer
	
	'**-Objective: Number identifying the policy/ quotation/ proposal
	'-Objetivo: Número identificativo de la póliza/ cotización/ solicitud
	Public nPolicy As Double
	
	'**-Objective: Number identifying the Certificate
	'-Objetivo: Número identificativo del certificado
	Public nCertif As Double
	
	'**-Objective: Code of the investment fund
	'-Objetivo: Código del fondo de inversión
	Public nFunds As Integer
	
	'**-Objective: Date which from the record is valid.
	'-Objetivo: Fecha de efecto del registro.
	Public dEffecdate As Date
	
	'**-Objective: Date when the record is cancelled.
	'-Objetivo: Fecha de anulación del registro.
	Public dNulldate As Date
	
	'**-Objective: Percentage of share in the Fund
	'-Objetivo: Porcentaje de participación de la póliza, en el fondo.
	Public nParticip As Double
	Public nIntProy As Double
	Public nIntProyVar As Double
	Public nIntProyVarCle As Double
	
	
	'**-Objective: Redirection indicator Sole values     1 - Affirmative    2 - Negative
	'-Objetivo: Indicador de redirección de salida Valores únicos    1 - Afirmativo    2 - Negativo
	Public sReaddress As String
	
	'**-Objective: Code of the user creating or updating the record.
	'-Objetivo: Código del usuario que crea o actualiza el registro.
	Public nUsercode As Integer
	
	'**-Objective: Quantity of investment units available
	'-Objetivo: Cantidad disponible de unidades de inversión
	Public nQuan_avail As Double
	Public nAmount As Double
	Public nBuy_cost As Double
	Public nSell_cost As Double
	Public sActivFound As String
	Public sDescript As String
	Public sIndicator As String
	Public sApv As String
	Public nOrigin As Integer
	Public sPortafol As String
	Public ncount As Integer
	Public sSel As String
	Public nUnitsChange As Double
	Public sBranch As String
	Public sProduct As String
	Public nBuysTot As Double
	Public nSellsTot As Double
	Public sVigen As String
	Public nTyp_Profitworker As Integer
	Public nAvailtobuy As Double
	Public sOrigin As String
	
	'%insValVI7002_k: Esta función se encarga de validar los datos introducidos en la forma (Header).
	Public Function insValVI7002_K(ByVal sCodispl As String, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sCompanyType As String = "") As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalVI7002_K As eRemoteDB.Execute

        On Error GoTo insvalVI7002_K_Err

        lrecinsvalVI7002_K = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalVI7002_K
            .StoredProcedure = "insVI7002PKG.insvalVI7002_K"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCompanyType", sCompanyType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            insValVI7002_K = .Confirm
        End With

insvalVI7002_K_Err:
        If Err.Number Then
            insValVI7002_K = "insvalVI7002_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalVI7002_K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalVI7002_K = Nothing

    End Function

    '**%Objective: VI7002 Page validations
    '%Objetivo: Función que permite efectuar las validaciones.
    Public Function insValVI7002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As String
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalVI7002 As eRemoteDB.Execute

        On Error GoTo insvalVI7002_Err

        lrecinsvalVI7002 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalVI7002
            .StoredProcedure = "insVI7002PKG.insvalVI7002"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage("VI7002", , , , , , lstrErrorAll)
            End If
            insValVI7002 = .Confirm
        End With

insvalVI7002_Err:
        If Err.Number Then
            insValVI7002 = "insvalVI7002: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lrecinsvalVI7002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsvalVI7002 = Nothing

    End Function
	
	'%Objetivo: Función que permite efectuar las actualizaciones del encabezado.
	Public Function insPostvi7002_k(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Double) As Boolean
		Dim lrecinspostvi7002_k As eRemoteDB.Execute
		
		On Error GoTo inspostvi7002_k_Err
		
		lrecinspostvi7002_k = New eRemoteDB.Execute
		
		'+ Se invoca el SP para validar los campos de la transacción
		
		With lrecinspostvi7002_k
			.StoredProcedure = "insVI7002PKG.INSPOSTVI7002_K"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostvi7002_k = .Run(False)
		End With
		
		
inspostvi7002_k_Err: 
		If Err.Number Then
			insPostvi7002_k = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinspostvi7002_k may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinspostvi7002_k = Nothing
		
	End Function
	
	'%Objetivo: Función que permite efectuar las actualizaciones del Folder.
	Public Function insPostvi7002(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Double) As Boolean
		Dim lrecinspostvi7002 As eRemoteDB.Execute
		
		On Error GoTo inspostvi7002_Err
		
		lrecinspostvi7002 = New eRemoteDB.Execute
		
		'+ Se invoca el SP para validar los campos de la transacción
		
		With lrecinspostvi7002
			.StoredProcedure = "insVI7002PKG.INSPOSTVI7002"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			
			insPostvi7002 = .Run(False)
		End With
		
		
inspostvi7002_Err: 
		If Err.Number Then
			insPostvi7002 = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinspostvi7002 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinspostvi7002 = Nothing
		
	End Function
	
	'%Objetivo: Función que permite efectuar las actualizaciones del Folder.
	Public Function insPostvi7002upd(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nUsercode As Double, ByVal nFunds As Integer, ByVal nOrigin As Integer, ByVal nParticip As Double, ByVal sSel As String, ByVal dNulldate As Date, ByVal sAction As String) As Boolean
		Dim lrecinspostvi7002upd As eRemoteDB.Execute
		
		On Error GoTo inspostvi7002upd_Err
		
		lrecinspostvi7002upd = New eRemoteDB.Execute
		
		'+ Se invoca el SP para validar los campos de la transacción
		
		With lrecinspostvi7002upd
			.StoredProcedure = "insVI7002PKG.INSPOSTVI7002UPD"
			.Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			.Parameters.Add("sAction", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
			insPostvi7002upd = .Run(False)
		End With
		
		
inspostvi7002upd_Err: 
		If Err.Number Then
			insPostvi7002upd = False
		End If
		On Error GoTo 0
		'UPGRADE_NOTE: Object lrecinspostvi7002upd may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		lrecinspostvi7002upd = Nothing
		
	End Function
End Class






