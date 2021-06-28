Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Funds_Pol_NET.Funds_Pol")> Public Class Funds_Pol
	'%-------------------------------------------------------%'
	'% $Workfile:: Funds_pol.cls                            $%'
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
	Public sShort_des As String
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
	
    '-Nueva variable NTYPEPROFILE tipo de inversor.
    Public nTypeProfile As Long
	


'**%Objective: Updates the percentage of participation of the policy in a fund
    '%Objetivo: Permite actualizar el porcentaje de participación de la póliza en el fondo
    Public Function UpdDynamic(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nSheet As Long, ByVal nField As Long, ByVal sValue As String, ByVal sCodispl As String, ByVal nUsercode As Long) As Boolean
        Dim lrecupdDynamic As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecupdDynamic = New eRemoteDB.Execute

        UpdDynamic = True

        With lrecupdDynamic
            .StoredProcedure = "INSPOSTGI1408"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSheet", nSheet, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nField", nField, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("sValue", sValue, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 200, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dValue", eRemoteDB.dtmNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValue", eRemoteDB.dblNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdDynamic = .Run(False)
        End With

        'UPGRADE_NOTE: Object lrecupdDynamic may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdDynamic = Nothing

        Exit Function
ErrorHandler:
        'UPGRADE_NOTE: Object lrecupdDynamic may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdDynamic = Nothing

        UpdDynamic = False
    End Function

    '-Variable usada en la ventana de distribución por fondos VI700F, llamada desde VI7000 y VI7004 - Rescates
    Public nSurrAmount As Double
    Public nTotal As Double

    '**%Objective: Reads all actives funds related to the policy
    '%Objetivo: Lee todos los fondos activos asociados a una póliza
    Public Function Find(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFunds As Integer) As Boolean
        Dim lrecreaFunds_pol As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecreaFunds_pol = New eRemoteDB.Execute

        Find = True

        With lrecreaFunds_pol
            .StoredProcedure = "reaFunds_pol_3"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Find = .Run

            If Find Then
                dNulldate = .FieldToClass("dNulldate")
                nParticip = .FieldToClass("nParticip")
                sDescript = .FieldToClass("sDescript")
                sReaddress = .FieldToClass("sReaddress")
                nQuan_avail = .FieldToClass("nQuan_avail")

                .RCloseRec()
            End If
        End With

        lrecreaFunds_pol = Nothing

        Exit Function
ErrorHandler:
        lrecreaFunds_pol = Nothing

        Find = False
    End Function

    '**%Objective: Reads the quantity of modification in the funds of the policy.
    '%Objetivo: Permite leer la cantidad de veces que han sido modificados los fondos de la póliza.
    Public Function FindFundsModify(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Decimal
        Dim lrecreaFunds_pol_2 As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecreaFunds_pol_2 = New eRemoteDB.Execute

        With lrecreaFunds_pol_2
            .StoredProcedure = "reaFunds_pol_2"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then

                '**+ Two redirection are one (Input and output)
                '+ Dos redirecciones conforman una sola (Entrada y salida)

                FindFundsModify = .FieldToClass("nModify")
                .RCloseRec()
            End If
        End With

        lrecreaFunds_pol_2 = Nothing

        Exit Function
ErrorHandler:
        lrecreaFunds_pol_2 = Nothing
    End Function


    '%Objetivo: Permite leer la cantidad de cuentas que fueron seleccionadas.
    Public Function Count_Cuentas(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Decimal
        Dim lrecreaCount_Cuentas As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecreaCount_Cuentas = New eRemoteDB.Execute

        Count_Cuentas = True

        With lrecreaCount_Cuentas
            .StoredProcedure = "reaCount_Cuentas"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Count_Cuentas = .Run

            If Count_Cuentas Then
                ncount = .FieldToClass("nCount")

                .RCloseRec()
            End If
        End With

        lrecreaCount_Cuentas = Nothing

        Exit Function
ErrorHandler:
        lrecreaCount_Cuentas = Nothing

        Count_Cuentas = False
    End Function



    '**%Objective: Add an element in the table Funds_pol
    '%Objetivo: Permite registrar un elemento en la tabla Funds_pol
    Public Function Add() As Boolean
        Dim lreccreFunds_pol As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lreccreFunds_pol = New eRemoteDB.Execute

        Add = True

        With lreccreFunds_pol
            .StoredProcedure = "creFunds_pol"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReaddress", sReaddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuan_avail", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sActivFound", sActivFound, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sApv", sApv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntProy", nIntProy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntProyVar", nIntProyVar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Add = .Run(False)
        End With

        lreccreFunds_pol = Nothing

        Exit Function
ErrorHandler:
        lreccreFunds_pol = Nothing

        Add = False
    End Function

    '**%Objective: Updates the percentage of participation of the policy in a fund
    '%Objetivo: Permite actualizar el porcentaje de participación de la póliza en el fondo
    Public Function Update() As Boolean
        Dim lrecupdFunds_pol As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecupdFunds_pol = New eRemoteDB.Execute

        Update = True

        With lrecupdFunds_pol
            .StoredProcedure = "updFunds_pol"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nParticip", nParticip, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndicator", sIndicator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sReaddress", sReaddress, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sActivFound", sActivFound, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sApv", sApv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntProy", nIntProy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntProyVar", nIntProyVar, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

        lrecupdFunds_pol = Nothing

        Exit Function
ErrorHandler:
        lrecupdFunds_pol = Nothing

        Update = False
    End Function

    '**%Objective: Deletes a fund related to the policy
    '%Objetivo: Permite eliminar un fondo asociado a una póliza
    Public Function Delete() As Boolean
        Dim lrecdelFunds_pol As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecdelFunds_pol = New eRemoteDB.Execute

        Delete = True

        With lrecdelFunds_pol
            .StoredProcedure = "delFunds_pol"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sIndicator", sIndicator, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sApv", sApv, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete = .Run(False)
        End With

        lrecdelFunds_pol = Nothing

        Exit Function
ErrorHandler:
        lrecdelFunds_pol = Nothing

        Delete = False
    End Function

    '%Objetivo: Permite verificar si un fondo se encuentra asociado a una póliza
    Public Function FindPolFund(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nFunds As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaFunds_pol_1 As eRemoteDB.Execute
        On Error GoTo ErrorHandler
        lrecreaFunds_pol_1 = New eRemoteDB.Execute
        With lrecreaFunds_pol_1
            .StoredProcedure = "reaFunds_p"
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(True) Then
                FindPolFund = True
                .RCloseRec()
            End If
        End With
        lrecreaFunds_pol_1 = Nothing
        Exit Function

ErrorHandler:
        lrecreaFunds_pol_1 = Nothing

        FindPolFund = False
    End Function

    '**%Objective: VI006 Page validations
    '%Objetivo: Función que permite efectuar las validaciones.
    Public Function insValVI006(ByVal sCodispl As String, Optional ByVal sSelected As String = "", Optional ByVal sWindowType As String = "", Optional ByVal nFunds As Integer = 0, Optional ByVal nPartic_min As Integer = 0, Optional ByVal nParticip As Double = 0, Optional ByVal sCertype As String = "", Optional ByVal nBranch As Integer = 0, Optional ByVal nProduct As Integer = 0, Optional ByVal nPolicy As Double = 0, Optional ByVal nCertif As Double = 0, Optional ByVal nTransaction As Integer = 0, Optional ByVal dEffecdate As Date = #12:00:00 AM#, Optional ByVal sRedirection As String = "", Optional ByVal sActivFound As String = "", Optional ByVal nOrigin As Integer = 0, Optional ByVal nIntProy As Double = 0, Optional ByVal nIntProyVar As Double = 0, Optional ByVal sVigen As String = "") As String
        Dim lblnValVI006 As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsFunds_pol As Funds_Pol
        Dim lclsFunds As Funds
        Dim lclsErrors As eFunctions.Errors
        Dim lclsvalfield As eFunctions.valField
        Dim lcolFundss As Fundss
        Dim lcolFunds_Pols As Funds_pols
        Dim lintParticip As Double
        Dim lintQuan_avail As Double
        Dim lintReaddress As Integer
        Dim lbnlParticip As Boolean
        Dim lblnPartic_min As Boolean
        Dim lclsPolicy_Win As Policy_Win
        Dim lintCountCtas As Integer
        Dim lintCountCtasFunds As Integer
        Dim lclsTab_ord_origin As Object
        Dim lintFunds_pol As Integer

        On Error GoTo insValVI006_err

        lclsProduct = New eProduct.Product
        lclsFunds_pol = New Funds_Pol
        lclsFunds = New Funds
        lclsErrors = New eFunctions.Errors
        lclsvalfield = New eFunctions.valField
        lcolFundss = New Fundss
        lcolFunds_Pols = New Funds_pols
        lclsPolicy_Win = New Policy_Win

        lclsvalfield.objErr = lclsErrors

        lintParticip = 0
        lintQuan_avail = 0
        lintReaddress = 0
        lblnValVI006 = True



        If sWindowType = "Popup" Then
            '+ Validación del campo " % Participación".
            '+ Si el fondo está seleccionado la partcipación debe estar llena
            If nParticip = eRemoteDB.Constants.intNull And sActivFound = "1" Then
                Call lclsErrors.ErrorMessage(sCodispl, 3402)
                lblnValVI006 = False
            Else

                '+Cualquier cambio de porcentaje (% )de distribución en un fondo no permitido para la venta según el producto en tratamiento,
                '+es permitido  solamente si el nuevo porcentaje (%) es igual a cero (0).
                If sCodispl = "VI7002" And sVigen = "1" And nParticip > 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 11235)
                    lblnValVI006 = False
                End If

                If lblnValVI006 And sActivFound = "1" And sCodispl <> "VI7002" Then
                    With lclsvalfield
                        .ErrEmpty = 1937
                        .Min = 1
                        .Max = 100
                        .EqualMin = True
                        .EqualMax = True
                        .Descript = "Participación"
                    End With

                    If Not lclsvalfield.ValNumber(nParticip) Then
                        lblnValVI006 = False
                    End If
                End If

                If lblnValVI006 Then
                    If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then
                        If lcolFundss.Find(nBranch, nProduct, dEffecdate) Then
                            For Each lclsFunds In lcolFundss
                                With lclsFunds
                                    If .nFunds = nFunds And .nOrigin = nOrigin Then
                                        If .nParticip <> nParticip Then
                                            If lclsProduct.sUlfchani = "2" Then
                                                lbnlParticip = True
                                                lblnValVI006 = False
                                            End If
                                        End If

                                        If .nPartic_min > nParticip And sActivFound = "1" Then
                                            lblnPartic_min = True
                                            lblnValVI006 = False
                                        End If
                                    End If
                                End With
                            Next lclsFunds
                        End If

                        If Not lbnlParticip Then
                            If sRedirection = "1" Then
                                If lcolFunds_Pols.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nOrigin) Then
                                    For Each lclsFunds_pol In lcolFunds_Pols
                                        With lclsFunds_pol
                                            If .nFunds = nFunds Then
                                                If .nParticip <> nParticip Then
                                                    lintReaddress = lintReaddress + 1
                                                End If

                                                If lclsProduct.nUlrmaxqu < .FindFundsModify(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate) + lintReaddress And (nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyAmendment) Then

                                                    '**+ More redirection permitted by the policy in the product designer must not be permitted
                                                    '+ No debe aceptar más redirecciones de las permitidas por póliza en el diseñador de productos

                                                    Call lclsErrors.ErrorMessage(sCodispl, 17008)

                                                    lblnValVI006 = True
                                                End If
                                            End If
                                        End With
                                    Next lclsFunds_pol
                                End If
                            End If
                        End If
                    End If
                    '                End If
                End If
            End If

            '**+ The percentage of participation must not be inferior to the minimum defined
            '**+ in the product designer
            '+ El porcentaje de participación no puede ser menor que el mínimo definido en
            '+ el diseñador de productos

            If lblnPartic_min Then
                Call lclsErrors.ErrorMessage(sCodispl, 17004)
            End If
                    If nIntProyVar < 0 Then
                            Call lclsErrors.ErrorMessage(sCodispl, 160162)
                        End If

            '**+ If in the product designer was specify that fund must not be modified
            '+ Si en el diseñador de productos se especificó que no se pueden cambiar
            '+ los fondos, no se puede cambiar este
            If lbnlParticip Then
                Call lclsErrors.ErrorMessage(sCodispl, 11128)
            End If
        Else
            lclsTab_ord_origin = eRemoteDB.NetHelper.CreateClassInstance("eBranches.Tab_Ord_Origins")
            Call lclsTab_ord_origin.Find(nBranch, nProduct)
            lintCountCtas = lclsTab_ord_origin.Count

            Call lclsFunds_pol.Count_Cuentas(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)
            lintCountCtasFunds = lclsFunds_pol.ncount

            If lclsProduct.sApv = "1" Then
                If lintCountCtasFunds = 0 Then
                    ' Call lclsErrors.ErrorMessage(sCodispl, 767092)
                    Call lclsErrors.ErrorMessage(sCodispl, 1928, , eFunctions.Errors.TextAlign.LeftAling, "debe seleccionar al menos un fondo por cada cuenta ")
                End If
            Else
                If lintCountCtas <> lintCountCtasFunds Then
                    ' Call lclsErrors.ErrorMessage(sCodispl, 767092)
                    Call lclsErrors.ErrorMessage(sCodispl, 1928, , eFunctions.Errors.TextAlign.LeftAling, "debe seleccionar al menos un fondo por cada cuenta ")
                End If
            End If

            If sSelected = String.Empty Then
                Call lclsErrors.ErrorMessage(sCodispl, 11084)
            Else
                If lcolFunds_Pols.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eRemoteDB.Constants.intNull) Then
                    For Each lclsFunds_pol In lcolFunds_Pols
                        With lclsFunds_pol
                            lintParticip = lintParticip + .nParticip
                            If nFunds = .nFunds Then
                                lintQuan_avail = lintQuan_avail + .nQuan_avail
                            End If
                                    If .nIntProyVar < 0 Then
                                                            Call lclsErrors.ErrorMessage(sCodispl, 160162)
                                                        End If
                        End With
                    Next lclsFunds_pol

                    If sWindowType <> "NormalDel" Then
                        If lclsProduct.sApv = "1" Then
                            If lintParticip <> 100 Then
                                Call lclsErrors.ErrorMessage(sCodispl, 3070)
                                If sCodispl = "VI006" Then
                                    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "1")
                                End If
                            End If
                        Else
                            If lintParticip <> 100 * lintCountCtas Then
                                Call lclsErrors.ErrorMessage(sCodispl, 3070)
                                If sCodispl = "VI006" Then
                                    Call lclsPolicy_Win.Add_PolicyWin(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nUsercode, sCodispl, "1")
                                End If
                            End If
                        End If
                    Else
                        If lintQuan_avail > 0 Then
                            Call lclsErrors.ErrorMessage(sCodispl, 56213)
                        End If
                    End If
                End If

                ' 978228 - Cantidad de fondos seleccionados excede la cantidad máxima de fondos permitidos para la simulacion
                If sCertype = "3" Then

                    lintFunds_pol = 0

                    If lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate) Then

                        lclsFunds_pol = Nothing
                        lcolFunds_Pols = Nothing

                        lclsFunds_pol = New Funds_Pol
                        lcolFunds_Pols = New Funds_pols

                        Call lcolFunds_Pols.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nOrigin)

                        'If lcolFunds_Pols.Count > lclsProduct.nUlfmaxqu_sim Then
                        '    Call lclsErrors.ErrorMessage(sCodispl, 978228)
                        'End If

                        lclsFunds_pol = Nothing
                        lcolFunds_Pols = Nothing
                    End If
                End If

            End If
        End If

        insValVI006 = lclsErrors.Confirm

insValVI006_err:
        If Err.Number Then
            insValVI006 = "insValVI006: " & Err.Description
        End If
        On Error GoTo 0
        lclsProduct = Nothing
        lclsFunds_pol = Nothing
        lclsErrors = Nothing
        lclsvalfield = Nothing
        lcolFundss = Nothing
        lcolFunds_Pols = Nothing
        lclsPolicy_Win = Nothing
        lclsTab_ord_origin = Nothing
    End Function

    '**%Objective: Updates the information in the frame VI006
    '%Objetivo: Permite actualizar los datos del frame VI006
    Public Function insPostVI006(ByVal sCodispl As String, ByVal sAction As String, ByVal nFunds As Integer, ByVal nParticip As Integer, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nUsercode As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal nTransaction As Integer, ByVal sActivFound As String, Optional ByVal sRedirection As String = "", Optional ByVal nOrigin As Integer = 0, Optional ByVal nIntProy As Double = 0, Optional ByVal nIntProyVar As Double = 0, Optional ByVal nTypeProfile As Long = 0) As Boolean
        Dim lintReaddress As Decimal
        Dim lintAuxParticip As Integer
        Dim lclsFunds_pols As Funds_pols
        Dim lclsUl_Move_Acc_pol As ePolicy.ul_Move_Acc_pol
        Dim lclsCurrent_pol As Curren_pol
        Dim lclsPolicy As Policy
        Dim lclsProduct As eProduct.Product

        On Error GoTo insPostVI006_err

        lclsFunds_pols = New Funds_pols
        lclsUl_Move_Acc_pol = New ePolicy.ul_Move_Acc_pol
        lclsCurrent_pol = New Curren_pol
        lclsPolicy = New Policy
        lclsProduct = New eProduct.Product

        insPostVI006 = True

        Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
        Call lclsPolicy.Find(sCertype, nBranch, nProduct, nPolicy)
        Call lclsFunds_pols.Find(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, nOrigin)

        With Me

            '**+ If is an underwritten, recovery o normal modification in the same day
            '+ Si es emisión, recuperación o modificación normal el mismo dia

            If (nTransaction = Constantes.PolTransac.clngPolicyIssue Or nTransaction = Constantes.PolTransac.clngPolicyProposal Or nTransaction = Constantes.PolTransac.clngPolicyQuotation Or nTransaction = Constantes.PolTransac.clngPolicyReissue Or nTransaction = Constantes.PolTransac.clngCertifIssue Or nTransaction = Constantes.PolTransac.clngCertifProposal Or nTransaction = Constantes.PolTransac.clngCertifQuotation Or nTransaction = Constantes.PolTransac.clngCertifReissue Or nTransaction = Constantes.PolTransac.clngRecuperation) Or ((nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyAmendment) And dEffecdate = lclsPolicy.dStartdate) Then
                .dNulldate = eRemoteDB.Constants.dtmNull
                .sIndicator = "1"
            Else

                '**+ If is a normal modification in the different day
                '+ Si es modificación normal a diferente dia

                If nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngCertifAmendment Then
                    .dNulldate = eRemoteDB.Constants.dtmNull
                    .sIndicator = "2"
                Else

                    '**+ If is a temporary modification in the same day
                    '+ Si es modificación temporal al mismo dia

                    If (nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment) And dEffecdate = lclsPolicy.dStartdate Then
                        .dNulldate = dNulldate
                        .sIndicator = "4"
                    Else

                        '**+ If is a temporary modification in the different day
                        '+ Si es modificación temporal a diferente dia

                        If nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment Then
                            .dNulldate = dNulldate
                            .sIndicator = "3"
                        End If
                    End If
                End If
            End If

            .sCertype = sCertype
            .nBranch = nBranch
            .nProduct = nProduct
            .nPolicy = nPolicy
            .nCertif = nCertif
            .dEffecdate = dEffecdate
            .nFunds = nFunds
            .nParticip = IIf(nParticip = eRemoteDB.Constants.intNull, 0, nParticip)
            .nUsercode = nUsercode
            .sActivFound = sActivFound
            .sApv = lclsProduct.sApv
            .nOrigin = nOrigin
            .nIntProy = IIf(nIntProy = eRemoteDB.Constants.intNull, 0, nIntProy)
            .nIntProyVar = IIf(nIntProyVar = eRemoteDB.Constants.intNull, 0, nIntProyVar)
            .nTypeProfile = nTypeProfile
            If sAction <> "Del" Then
                If Not lclsFunds_pols.FindItem(nFunds, nParticip, nOrigin, nIntProy, nIntProyVar) Then
                    .sReaddress = "0"
                    .Add()
                Else
                    .sReaddress = "0"

                    If sRedirection = "1" Then
                        If (nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment) Then
                            .sReaddress = "1"
                        End If
                    End If

                    .Update()
                End If
            Else
                .Delete()
            End If
        End With

        If sRedirection = "1" Then
            If (nTransaction = Constantes.PolTransac.clngCertifAmendment Or nTransaction = Constantes.PolTransac.clngPolicyAmendment Or nTransaction = Constantes.PolTransac.clngTempCertifAmendment Or nTransaction = Constantes.PolTransac.clngTempPolicyAmendment) Then
                lintReaddress = FindFundsModify(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate)

                If (lintReaddress > lclsProduct.nUlrmaxqu Or Not insPeriodFree(nBranch, nProduct, nPolicy, nCertif, dEffecdate)) And lclsProduct.nUlrcharg > 0 Then

                    With lclsUl_Move_Acc_pol
                        .sCertype = sCertype
                        .nBranch = nBranch
                        .nProduct = nProduct
                        .nPolicy = nPolicy
                        .nCertif = nCertif

                        If lclsCurrent_pol.Find(nPolicy, nBranch, nProduct, sCertype, nCertif, dEffecdate) Then
                            Call lclsCurrent_pol.Val_Curren_pol(0)

                            .nCurrency = lclsCurrent_pol.nCurrency
                        Else
                            .nCurrency = CInt("1")
                        End If

                        .dOperDate = dEffecdate
                        .nType_Move = 15
                        .nIdconsec = 0

                        .nOutAmount = lclsProduct.nUlrcharg
                        .nUsercode = nUsercode
                        .nReceipt = eRemoteDB.Constants.intNull
                        .sPayer = String.Empty
                        .nInstitution = eRemoteDB.Constants.intNull
                        .nIntermei = 2
                        .nOrigin = eRemoteDB.Constants.intNull
                        .dDate_Origin = dEffecdate
                        .nInvested = 2
                        .dPosted = eRemoteDB.Constants.dtmNull
                        .nLed_Compan = eRemoteDB.Constants.intNull
                        .sAccount = String.Empty
                        .sAux_Accoun = String.Empty

                        insPostVI006 = .insApplyChargeRedi
                    End With
                End If

                With lclsUl_Move_Acc_pol
                    .sCertype = sCertype
                    .nBranch = nBranch
                    .nProduct = nProduct
                    .nPolicy = nPolicy
                    .nCertif = nCertif
                    .nUsercode = nUsercode
                    .dOperDate = dEffecdate
                    insPostVI006 = .insApplyRediHis
                End With
            End If
        End If
        If nTypeProfile > 0 Then
            UpdDynamic(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, 90078, 3, nTypeProfile, "INT90078", nUsercode)
        End If

insPostVI006_err:
        If Err.Number Then
            insPostVI006 = False
        End If
        On Error GoTo 0
        lclsFunds_pols = Nothing
        lclsUl_Move_Acc_pol = Nothing
        lclsCurrent_pol = Nothing
        lclsPolicy = Nothing
        lclsProduct = Nothing

    End Function

    '**%Objective: Updates the quantity availables in the fund
    '%Objetivo: Permite actualizar la cantidad de unidades disponibles de la poliza en el fondo
    Public Function UpdateAvail() As Boolean
        Dim lrecupdFunds_pol As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecupdFunds_pol = New eRemoteDB.Execute

        UpdateAvail = True

        With lrecupdFunds_pol
            .StoredProcedure = "updFunds_polAvail"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nQuan_avail", nQuan_avail, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            UpdateAvail = .Run(False)
        End With

        lrecupdFunds_pol = Nothing

        Exit Function
ErrorHandler:
        lrecupdFunds_pol = Nothing

        UpdateAvail = False
    End Function

    '**%Objective: This function return true if the effective date of the
    '**%           operation is between the free charges period
    '%Objetivo: Función que retorna verdadero si la fecha efectiva de
    '%          la transacción está contemplada dentro del periodo libre de cargo
    Public Function insPeriodFree(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim mresulDate As Date
        Dim ldtmDate As Date

        On Error GoTo ErrorHandler
        insPeriodFree = True

        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat

        With lclsProduct
            Call .FindProduct_li(nBranch, nProduct, dEffecdate)

            If nCertif = 0 Then
                Call lclsPolicy.Find("2", nBranch, nProduct, nPolicy)

                ldtmDate = lclsPolicy.dDate_Origi
            Else
                Call lclsCertificat.Find("2", nBranch, nProduct, nPolicy, nCertif)

                ldtmDate = lclsCertificat.dDate_Origi
            End If

            If .nUlredper <> 0 And .nUlredper <> eRemoteDB.Constants.intNull Then
                Select Case .nUlredper

                    '**+ It adds the charge frecuency to the date origin of the policy/certificate
                    '+ Se le suma la frecuencia a la fecha de efecto de la poliza/certificado

                    Case Funds.ePayFrecuency.esdMonthly
                        mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, ldtmDate)

                    Case Funds.ePayFrecuency.esdAnualy
                        mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 12, ldtmDate)

                    Case Funds.ePayFrecuency.esdSemestral
                        mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 6, ldtmDate)

                    Case Funds.ePayFrecuency.esdTrimestral
                        mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 3, ldtmDate)

                    Case Funds.ePayFrecuency.esdBiMestral
                        mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 2, ldtmDate)

                    Case Else
                        mresulDate = DateAdd(Microsoft.VisualBasic.DateInterval.Month, 1, ldtmDate)
                End Select

                '**+ if the result date is inferior to the transacton date
                '**+ then the redirection cost will no be collected
                '+ Si la fecha resultante es menor que la fecha de la transacción
                '+ entonces no se cobrara el costo por redirección

                If mresulDate < dEffecdate Then
                    insPeriodFree = False
                End If
            End If
        End With

        lclsProduct = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing

        Exit Function
ErrorHandler:
        lclsProduct = Nothing
        lclsPolicy = Nothing
        lclsCertificat = Nothing

        insPeriodFree = False
    End Function


    '**%Objective: calculates the available amount to buy units
    '%Objetivo: Calcula el importe disponible para calcular unidades
    Public Function insCalAvailable(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer) As Double
        Dim lreccreFunds As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lreccreFunds = New eRemoteDB.Execute

        insCalAvailable = 0

        With lreccreFunds
            .StoredProcedure = "INS_CAL_POL_ACC_BALANCE_1"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nResult", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nError", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBalance", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                If .Parameters("nError").Value = 0 Then
                    insCalAvailable = .Parameters("nBalance").Value
                Else
                    insCalAvailable = 0
                End If
            End If
        End With

        lreccreFunds = Nothing

        Exit Function

ErrorHandler:
        lreccreFunds = Nothing
        insCalAvailable = 0
    End Function

    '**%Objective: calculates the available amount to buy units
    '%Objetivo: Calcula el importe disponible para calcular unidades
    Public Function insCalAvailable_Contrib(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer) As Decimal
        Dim lreccreFunds As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lreccreFunds = New eRemoteDB.Execute

        insCalAvailable_Contrib = 0

        With lreccreFunds
            .StoredProcedure = "INS_CAL_POL_CONTR_EXT"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBalance", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                insCalAvailable_Contrib = .Parameters("nBalance").Value
            Else
                insCalAvailable_Contrib = 0
            End If

        End With

        lreccreFunds = Nothing

        Exit Function

ErrorHandler:
        lreccreFunds = Nothing
        insCalAvailable_Contrib = 0
    End Function

    '**%Objective: calculates the available amount to buy units
    '%Objetivo: Calcula el importe disponible para calcular unidades
    Public Function insCalPolAccBalance(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nCurrency As Double, ByVal nOrigin As Integer) As Decimal
        Dim lrecreaFunds_pol As eRemoteDB.Execute

        On Error GoTo insCalPolAccBalance_err

        lrecreaFunds_pol = New eRemoteDB.Execute

        insCalPolAccBalance = 0

        With lrecreaFunds_pol
            .StoredProcedure = "insCalPolAccBalance"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nResult", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nError", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBalance", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBal_Saving", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBal_Units", 0, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(False) Then
                If .Parameters("nError").Value = 0 Then
                    insCalPolAccBalance = .Parameters("nBalance").Value
                Else
                    insCalPolAccBalance = 0
                End If
            End If
        End With

insCalPolAccBalance_err:
        If Err.Number Then
            insCalPolAccBalance = 0
        End If
        On Error GoTo 0
        lrecreaFunds_pol = Nothing
    End Function


    '%insValVI010_k: Esta función se encarga de validar los datos introducidos en la forma VI010 (Header).
    Public Function insValVI010_k(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCompanyType As String, ByVal nCurrency As Integer, ByVal nOrigin As Double, ByVal sProcessType As String, Optional ByVal sCodispl_orig As String = "") As String
        Dim lclsFunds As ePolicy.Funds
        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsUl_Move_Acc_pol As ePolicy.ul_Move_Acc_pol
        Dim lobjErrors As eFunctions.Errors
        Dim lblnError As Boolean
        Dim ldtmDate As Date
        Dim nProponum As Double



        On Error GoTo insValVI010_K_Err
        lclsFunds = New ePolicy.Funds
        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsUl_Move_Acc_pol = New ePolicy.ul_Move_Acc_pol
        lobjErrors = New eFunctions.Errors

        lblnError = False

        '**+ Validate the field Line of business
        '+Se valida el campo Ramo
        With lobjErrors
            If nBranch <= 0 Then
                .ErrorMessage(sCodispl, 1022)
                lblnError = True
            End If

            '**+ Validate that the field Product.
            '+ Se valida que el campo Producto.
            If nProduct <= 0 Then
                .ErrorMessage(sCodispl, 1014)
                lblnError = True
            Else
                '**+ Validate that the product corresponds to life or combined
                '+ Se valida que el producto corresponda a vida o combinado
                Call lclsProduct.insValProdMaster(nBranch, nProduct)

                If lclsProduct.blnError Then
                    If CStr(lclsProduct.sBrancht) <> "1" And CStr(lclsProduct.sBrancht) <> "2" And CStr(lclsProduct.sBrancht) <> "5" Then

                        .ErrorMessage(sCodispl, 3403)
                        lblnError = True
                    Else

                        '**+ Read the Funds associated to the Line of business_Product to the given date
                        '+ Leer de Funds los fondos asociados al Ramo-Producto a la fecha dada
                        If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                            If Not lclsFunds.Find(nBranch, nProduct, dEffecdate) Then
                                .ErrorMessage(sCodispl, 17002)
                                lblnError = True
                            End If
                        End If
                    End If
                End If

                With lclsProduct
                    If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                        If .nProdClas <> 3 And .nProdClas <> 4 Then
                            lobjErrors.ErrorMessage(sCodispl, 70123)
                            lblnError = True
                        End If
                        If .sApv = "1" Then
                            lobjErrors.ErrorMessage(sCodispl, 3406, , eFunctions.Errors.TextAlign.RigthAling, " : No debe ser apv")
                            lblnError = True
                        End If
                    End If
                End With
            End If

            '**+ Validate that the field Policy
            '+Se valida que el campo Póliza.
            If Not lblnError Then
                If nPolicy <= 0 Then
                    '+Si el proceso es preliminar
                    If sProcessType = "2" Then
                        .ErrorMessage(sCodispl, 3003)
                    End If
                    lblnError = True
                Else

                    '**+ Validate that it is valid policy
                    '+ Se valida que sea una póliza válida
                    If Not lclsPolicy.FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, sCompanyType) Then
                        .ErrorMessage(sCodispl, 3001)
                        lblnError = True
                    Else
                        If lclsPolicy.sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Or lclsPolicy.sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrInvalid) Then
                            .ErrorMessage(sCodispl, 3720)
                            lblnError = True
                        Else

                            '**+ Verify that the policy is not anulled
                            '+ Verificar que la póliza no esté anulada
                            If lclsPolicy.dNulldate <> eRemoteDB.Constants.dtmNull Then
                                .ErrorMessage(sCodispl, 3098)
                                lblnError = True
                            Else
                                Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
                            End If
                        End If
                    End If
                End If
            End If

            '**+ Validate the field Certificate.
            '+Se valida que el campo Certificado.

            If Not lblnError Then
                If nCertif <= 0 Then
                    If lclsPolicy.sPolitype <> CStr(Constantes.ePoliType.cstrIndividual) Then
                        '+Si el proceso es preliminar
                        If sProcessType = "2" Then
                            .ErrorMessage(sCodispl, 3006)
                        End If
                        lblnError = True
                    End If
                Else
                    If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                        .ErrorMessage(sCodispl, 3010)
                        lblnError = True
                    Else
                        '**+ Validate that the certificate is valid
                        '+ Se válida que el certificado sea válido
                        If lclsCertificat.sStatusva = "3" Or lclsCertificat.sStatusva = "2" Then
                            .ErrorMessage(sCodispl, 750044)
                            lblnError = True
                        Else
                            If lclsCertificat.dNulldate <> eRemoteDB.Constants.dtmNull Then
                                .ErrorMessage(sCodispl, 3099)
                                lblnError = True
                            End If
                        End If
                    End If
                End If
            End If

            '**+ The field date must be full
            '+El campo fecha debe estar lleno
            If dEffecdate = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 3404)
                lblnError = True
            End If

            '**+ Validate the field Origin
            '+Se valida el campo Origen
            If sProcessType = "2" Then
                If nOrigin <= 0 Then
                    .ErrorMessage(sCodispl, 70089)
                    lblnError = True
                End If
            End If

            If Not lblnError Then
                If sProcessType = "2" Then
                    If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                        '**+ Validate that the date is posterior to the effect date of the Policy
                        '+Se válida que la fecha sea posterior a la fecha de efecto de la Póliza
                        If nCertif > 0 Then
                            ldtmDate = lclsCertificat.dDate_Origi
                        Else
                            ldtmDate = lclsPolicy.dDate_Origi
                        End If

                        If dEffecdate < ldtmDate Then
                            .ErrorMessage(sCodispl, 3262)
                        End If

                        '**+ The date of the transaction must be posterior or equal to the
                        '**+ date of the last making of funds change.
                        '+La fecha de la transacción debe ser posterior o igual a la
                        '+fecha de la última realización de cambios de fondos.
                        lclsUl_Move_Acc_pol.nBranch = nBranch
                        lclsUl_Move_Acc_pol.nProduct = nProduct
                        lclsUl_Move_Acc_pol.nPolicy = nPolicy
                        lclsUl_Move_Acc_pol.nCertif = nCertif
                        lclsUl_Move_Acc_pol.nCurrency = nCurrency
                        lclsUl_Move_Acc_pol.nType_Move = 0

                        '+Si encontro al menos un movimiento
                        If lclsUl_Move_Acc_pol.FindLastMove Then

                            '+Cambio debe ser posterior a ultimo movimiento
                            If lclsUl_Move_Acc_pol.dOperDate > dEffecdate Then
                                lobjErrors.ErrorMessage(sCodispl, 3090)
                                lblnError = True
                            End If

                            If Not lblnError Then
                                '+Se recupera la fecha en que se puede realizar el sgte switch (segun el ultimo realizado)
                                '+y la cantidad de switch del periodo indicado en el producto
                                Call lclsUl_Move_Acc_pol.insGetPeriodInfo(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eCashBank.Move_Acc.eMove_Type.esdSwitch, lclsProduct.nULSwmqt, lclsProduct.nULswmqtper, lclsProduct.nULswmaxper, ldtmDate, lclsFunds.nCantSwitch)

                                '+Se verifica que no se hayan realizado aun los switch posibles por periodo
                                If lclsProduct.nUlsmaxqu > 0 And lclsFunds.nCantSwitch >= lclsProduct.nUlsmaxqu Then
                                    lobjErrors.ErrorMessage(sCodispl, 17008)
                                End If

                            End If

                        End If
                    End If
                End If
            End If
            If sCodispl_orig = String.Empty And sProcessType = "2" Then
                nProponum = ValProp_pend(sCertype, nBranch, nProduct, nPolicy, nCertif)
                If nProponum > 0 Then
                    lobjErrors.ErrorMessage(sCodispl, 55780, , eFunctions.Errors.TextAlign.RigthAling, ".La propuesta: " & nProponum & " se debe actualizar o anular en la transaccion de Tratamiento de cotizaciones/propuesta")
                End If
            End If
            insValVI010_k = .Confirm
        End With

        'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsUl_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsUl_Move_Acc_pol = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValVI010_K_Err:
        If Err.Number Then
            insValVI010_k = "insValVI010_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsUl_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsUl_Move_Acc_pol = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '%insValVI016_k: Esta función se encarga de validar los datos introducidos en la forma VI016 Swtich -APV (Header).
    Public Function insValVI016_k(ByVal sCodispl As String, ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal sCompanyType As String, ByVal nCurrency As Integer, ByVal nOrigin As Double, ByVal sProcessType As String, Optional ByVal sCodispl_orig As String = "") As String
        Dim lclsFunds As ePolicy.Funds
        Dim lclsProduct As eProduct.Product
        Dim lclsPolicy As ePolicy.Policy
        Dim lclsCertificat As ePolicy.Certificat
        Dim lclsUl_Move_Acc_pol As ePolicy.ul_Move_Acc_pol
        Dim lobjErrors As eFunctions.Errors
        Dim lblnError As Boolean
        Dim ldtmDate As Date
        Dim nProponum As Double

        On Error GoTo insValVI016_K_Err
        lclsFunds = New ePolicy.Funds
        lclsProduct = New eProduct.Product
        lclsPolicy = New ePolicy.Policy
        lclsCertificat = New ePolicy.Certificat
        lclsUl_Move_Acc_pol = New ePolicy.ul_Move_Acc_pol
        lobjErrors = New eFunctions.Errors

        lblnError = False

        '**+ Validate the field Line of business
        '+Se valida el campo Ramo
        With lobjErrors
            If nBranch <= 0 Then
                .ErrorMessage(sCodispl, 1022)
                lblnError = True
            End If

            '**+ Validate that the field Product.
            '+ Se valida que el campo Producto.
            If nProduct <= 0 Then
                .ErrorMessage(sCodispl, 1014)
                lblnError = True
            Else
                '**+ Validate that the product corresponds to life or combined
                '+ Se valida que el producto corresponda a vida o combinado
                Call lclsProduct.insValProdMaster(nBranch, nProduct)

                If lclsProduct.blnError Then
                    If CStr(lclsProduct.sBrancht) <> "1" And CStr(lclsProduct.sBrancht) <> "2" And CStr(lclsProduct.sBrancht) <> "5" Then

                        .ErrorMessage(sCodispl, 3403)
                        lblnError = True
                    Else

                        '**+ Read the Funds associated to the Line of business_Product to the given date
                        '+ Leer de Funds los fondos asociados al Ramo-Producto a la fecha dada
                        If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                            If Not lclsFunds.Find(nBranch, nProduct, dEffecdate) Then
                                .ErrorMessage(sCodispl, 17002)
                                lblnError = True
                            End If
                        End If
                    End If
                End If

                With lclsProduct
                    If .FindProduct_li(nBranch, nProduct, dEffecdate) Then
                        If .nProdClas <> 3 And .nProdClas <> 4 Then
                            lobjErrors.ErrorMessage(sCodispl, 70123)
                            lblnError = True
                        End If

                        If (.sApv = "2" Or .sApv = "") Then
                            lobjErrors.ErrorMessage(sCodispl, 70177)
                            lblnError = True
                        End If
                    End If
                End With
            End If

            '**+ Validate that the field Policy
            '+Se valida que el campo Póliza.
            If Not lblnError Then
                If nPolicy <= 0 Then
                    '+Si el proceso es preliminar
                    If sProcessType = "2" Then
                        .ErrorMessage(sCodispl, 3003)
                    End If
                    lblnError = True
                Else

                    '**+ Validate that it is valid policy
                    '+ Se valida que sea una póliza válida
                    If Not lclsPolicy.FindPolicyOfficeName(sCertype, nBranch, nProduct, nPolicy, sCompanyType) Then
                        .ErrorMessage(sCodispl, 3001)
                        lblnError = True
                    Else
                        If lclsPolicy.sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrIncomplete) Or lclsPolicy.sStatus_pol = CStr(Policy.TypeStatus_Pol.cstrInvalid) Then
                            .ErrorMessage(sCodispl, 3720)
                            lblnError = True
                        Else

                            '**+ Verify that the policy is not anulled
                            '+ Verificar que la póliza no esté anulada
                            If lclsPolicy.dNulldate <> eRemoteDB.Constants.dtmNull Then
                                .ErrorMessage(sCodispl, 3098)
                                lblnError = True
                            Else
                                Call lclsProduct.FindProduct_li(nBranch, nProduct, dEffecdate)
                            End If
                        End If
                    End If
                End If
            End If

            '**+ Validate the field Certificate.
            '+Se valida que el campo Certificado.

            If Not lblnError Then
                If nCertif <= 0 Then
                    If lclsPolicy.sPolitype <> CStr(Constantes.ePoliType.cstrIndividual) Then
                        '+Si el proceso es preliminar
                        If sProcessType = "2" Then
                            .ErrorMessage(sCodispl, 3006)
                        End If
                        lblnError = True
                    End If
                Else
                    If Not lclsCertificat.Find(sCertype, nBranch, nProduct, nPolicy, nCertif) Then
                        .ErrorMessage(sCodispl, 3010)
                        lblnError = True
                    Else
                        '**+ Validate that the certificate is valid
                        '+ Se válida que el certificado sea válido
                        If lclsCertificat.sStatusva = "3" Or lclsCertificat.sStatusva = "2" Then
                            .ErrorMessage(sCodispl, 750044)
                            lblnError = True
                        Else
                            If lclsCertificat.dNulldate <> eRemoteDB.Constants.dtmNull Then
                                .ErrorMessage(sCodispl, 3099)
                                lblnError = True
                            End If
                        End If
                    End If
                End If
            End If

            '**+ The field date must be full
            '+El campo fecha debe estar lleno
            If dEffecdate = eRemoteDB.Constants.dtmNull Then
                .ErrorMessage(sCodispl, 3404)
                lblnError = True
            End If

            '**+ Validate the field Origin
            '+Se valida el campo Origen
            If sProcessType = "2" Then
                If nOrigin <= 0 Then
                    .ErrorMessage(sCodispl, 70089)
                    lblnError = True
                End If
            End If

            If Not lblnError Then
                If sProcessType = "2" Then
                    If dEffecdate <> eRemoteDB.Constants.dtmNull Then
                        '**+ Validate that the date is posterior to the effect date of the Policy
                        '+Se válida que la fecha sea posterior a la fecha de efecto de la Póliza
                        If nCertif > 0 Then
                            ldtmDate = lclsCertificat.dDate_Origi
                        Else
                            ldtmDate = lclsPolicy.dDate_Origi
                        End If

                        If dEffecdate < ldtmDate Then
                            .ErrorMessage(sCodispl, 3262)
                        End If

                        '**+ The date of the transaction must be posterior or equal to the
                        '**+ date of the last making of funds change.
                        '+La fecha de la transacción debe ser posterior o igual a la
                        '+fecha de la última realización de cambios de fondos.
                        lclsUl_Move_Acc_pol.nBranch = nBranch
                        lclsUl_Move_Acc_pol.nProduct = nProduct
                        lclsUl_Move_Acc_pol.nPolicy = nPolicy
                        lclsUl_Move_Acc_pol.nCertif = nCertif
                        lclsUl_Move_Acc_pol.nCurrency = nCurrency
                        lclsUl_Move_Acc_pol.nType_Move = 0

                        '+Si encontro al menos un movimiento
                        If lclsUl_Move_Acc_pol.FindLastMove Then

                            '+Cambio debe ser posterior a ultimo movimiento
                            If lclsUl_Move_Acc_pol.dOperDate > dEffecdate Then
                                lobjErrors.ErrorMessage(sCodispl, 3090)
                                lblnError = True
                            End If

                            If Not lblnError Then
                                '+Se recupera la fecha en que se puede realizar el sgte switch (segun el ultimo realizado)
                                '+y la cantidad de switch del periodo indicado en el producto
                                Call lclsUl_Move_Acc_pol.insGetPeriodInfo(sCertype, nBranch, nProduct, nPolicy, nCertif, dEffecdate, eCashBank.Move_Acc.eMove_Type.esdSwitch, lclsProduct.nULSwmqt, lclsProduct.nULswmqtper, lclsProduct.nULswmaxper, ldtmDate, lclsFunds.nCantSwitch)

                                '+Se verifica que no se hayan realizado aun los switch posibles por periodo
                                If lclsProduct.nUlsmaxqu > 0 And lclsFunds.nCantSwitch >= lclsProduct.nUlsmaxqu Then
                                    lobjErrors.ErrorMessage(sCodispl, 17008)
                                End If

                            End If

                        End If
                    End If
                End If
            End If
            If sCodispl_orig = String.Empty And sProcessType = "2" Then
                nProponum = ValProp_pend(sCertype, nBranch, nProduct, nPolicy, nCertif)
                If nProponum > 0 Then
                    lobjErrors.ErrorMessage(sCodispl, 55780, , eFunctions.Errors.TextAlign.RigthAling, ".La propuesta: " & nProponum & " se debe actualizar o anular en la transaccion de Tratamiento de cotizaciones/propuesta")
                End If
            End If

            insValVI016_k = .Confirm
        End With

        'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsUl_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsUl_Move_Acc_pol = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing

insValVI016_K_Err:
        If Err.Number Then
            insValVI016_k = "insValVI016_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds = Nothing
        'UPGRADE_NOTE: Object lclsProduct may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsProduct = Nothing
        'UPGRADE_NOTE: Object lclsPolicy may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPolicy = Nothing
        'UPGRADE_NOTE: Object lclsCertificat may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsCertificat = Nothing
        'UPGRADE_NOTE: Object lclsUl_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsUl_Move_Acc_pol = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '**%Objective: This function is in charge of validate the introduced dat in the VI010 form (Form)
    '%Objetivo: Esta función se encarga de validar los datos introducidos la forma VI010 (Folder)
    Public Function insValVI016A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sCodispl As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
        Dim lrecinsValVI016A As eRemoteDB.Execute

        On Error GoTo insValVI016A_Err

        lrecinsValVI016A = New eRemoteDB.Execute

        '+ Definición de store procedure InsValVI016A
        With lrecinsValVI016A
            .StoredProcedure = "InsValVI016A"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        'UPGRADE_NOTE: Object lrecinsValVI016A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValVI016A = Nothing

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            insValVI016A = .Confirm
        End With

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValVI016A_Err:
        If Err.Number Then
            insValVI016A = "insValVI016A: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

    End Function

    '**%Objective: This function is in charge of validate the introduced dat in the VI010 form (Form)
    '%Objetivo: Esta función se encarga de validar los datos introducidos la forma VI010 (Folder)
    Public Function insValVI010(ByVal sCodispl As String, ByVal sWindowsType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUnits As Double, ByVal nSignal As Integer, ByVal nUnitsChange As Double, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sActivFound As String, ByVal nAvailable As Double, Optional ByVal nValueChange As Double = 0, Optional ByVal nValueChange_aux As Double = 0, Optional ByVal nFunds As Integer = 0) As String
        Dim lclsFunds As ePolicy.Funds
        Dim lobjErrors As eFunctions.Errors
        Dim lclsUl_Move_Acc_pol As ePolicy.ul_Move_Acc_pol
        Dim lblnVal As Boolean
        Dim nErrorNum As Double
        On Error GoTo insValVI010_Err

        lclsUl_Move_Acc_pol = New ePolicy.ul_Move_Acc_pol
        lclsFunds = New ePolicy.Funds
        lobjErrors = New eFunctions.Errors

        '**- Variables that will count the number of units bought and sell
        '- Variables que contaran el numero de unidades
        '- compradas y vendidas.
        lclsFunds.nUnitsPurchase = 0
        lclsFunds.nUnitsSales = 0
        lblnVal = False

        With lobjErrors
            '+ En caso de comprar unidades debe existir unidades disponibles para la póliza en el fondo
            '+ Compra de unidades
            If nSignal = 1 Then
                '+   debe pertenecer a portafolio activo
                'If sActivFound <> "1" Then
                '    .ErrorMessage sCodispl, 56205
                '    lblnVal = True
                'End If

                If Not lblnVal Then
                    '+ La cantidad de unidades a comprar (+) debe ser menor o igual a las que tiene el  fondo
                    If CDec(nUnitsChange) <= 0 Then
                        .ErrorMessage(sCodispl, 10291)
                    Else
                        lclsFunds.nUnitsPurchase = lclsFunds.nUnitsPurchase + CDec(nUnitsChange)

                        If nValueChange > (nAvailable + nValueChange_aux) Then
                            .ErrorMessage(sCodispl, 70120)
                        End If
                    End If
                End If

                ' Venta de unidades
            ElseIf nSignal = 2 Then
                '+ En caso de vender se efectua la misma validación, pero en el stock de fondos.
                If nUnits <= 0 Then
                    .ErrorMessage(sCodispl, 3761)
                ElseIf CDec(nUnitsChange) <= 0 Then
                    .ErrorMessage(sCodispl, 10291)
                    '+ En caso de efectuar una venta de unidades, el valor de
                    '+ las unidades no debe ser mayor que la actual de unidades
                ElseIf CDec(nUnitsChange) > CDec(nUnits) Then
                    .ErrorMessage(sCodispl, 3761)
                Else
                    If CDec(nUnitsChange) = CDec(nUnits) And sActivFound <> "1" And sWindowsType = "PopUp" Then
                        .ErrorMessage(sCodispl, 56206)
                    End If
                End If
                '+ si la operación es venta no deben existir abonos pendientes
                lclsFunds.nUnitsSales = lclsFunds.nUnitsSales + CDec(nUnitsChange)
                If lclsUl_Move_Acc_pol.FindUl_Move_Acc_pol_Api("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                    If lclsUl_Move_Acc_pol.sExiulmap = "1" Then
                        .ErrorMessage(sCodispl, 56207)
                    End If
                End If
            End If
            nErrorNum = ValProp_pend_upd(sCertype, nBranch, nProduct, nPolicy, nCertif, nFunds, nSignal)
            If nErrorNum > 0 Then
                .ErrorMessage(sCodispl, nErrorNum)
            End If
            insValVI010 = .Confirm
        End With

insValVI010_Err:
        If Err.Number Then
            insValVI010 = "insValVI010: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsUl_Move_Acc_pol may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsUl_Move_Acc_pol = Nothing
        'UPGRADE_NOTE: Object lclsFunds may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFunds = Nothing
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '**%Objective: This function is in charge of validate the introduced dat in the VI010 form (Form)
    '%Objetivo: Esta función se encarga de validar los datos introducidos la forma VI010 (Folder)
    Public Function insValVI010A(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sCodispl As String) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = String.Empty
        Dim lrecinsValVI010A As eRemoteDB.Execute

        On Error GoTo insValVI010A_Err

        lrecinsValVI010A = New eRemoteDB.Execute

        '+ Definición de store procedure InsValVI7000
        With lrecinsValVI010A
            .StoredProcedure = "InsValVI010A"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With

        'UPGRADE_NOTE: Object lrecinsValVI010A may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValVI010A = Nothing

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            insValVI010A = .Confirm
        End With

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

insValVI010A_Err:
        If Err.Number Then
            insValVI010A = "insValVI010A: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing

    End Function

    '**%Objective: This function is in charge of validate the introduced dat in the VI016 form (Form)
    '%Objetivo: Esta función se encarga de validar los datos introducidos la forma VI016 (Folder)
    Public Function insValVI016(ByVal sCodispl As String, ByVal sWindowsType As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal dEffecdate As Date, ByVal nUnits As Double, ByVal nSignal As Integer, ByVal nUnitsChange As Double, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal sActivFound As String, ByVal nAvailable As Double, ByVal nValueChange As Double, ByVal nTyp_Profitworker As Integer, ByVal nValueChange_aux As Double, ByVal nFunds As Integer) As String
        Dim lclsFunds As ePolicy.Funds
        Dim lobjErrors As eFunctions.Errors
        Dim lclsUl_Move_Acc_pol As ePolicy.ul_Move_Acc_pol
        Dim lblnVal As Boolean
        Dim nErrorNum As Double

        On Error GoTo insValVI016_Err

        lclsUl_Move_Acc_pol = New ePolicy.ul_Move_Acc_pol
        lclsFunds = New ePolicy.Funds
        lobjErrors = New eFunctions.Errors

        '**- Variables that will count the number of units bought and sell
        '- Variables que contaran el numero de unidades
        '- compradas y vendidas.
        lclsFunds.nUnitsPurchase = 0
        lclsFunds.nUnitsSales = 0
        lblnVal = False

        With lobjErrors

            If nTyp_Profitworker = eRemoteDB.Constants.intNull Or nTyp_Profitworker = 0 Then
                .ErrorMessage(sCodispl, 80003)
            End If

            '+ En caso de comprar unidades debe existir unidades disponibles para la póliza en el fondo
            '+ Compra de unidades
            If nSignal = 1 Then
                '+   debe pertenecer a portafolio activo
                'If sActivFound <> "1" Then
                '    .ErrorMessage sCodispl, 56205
                '    lblnVal = True
                'End If

                If Not lblnVal Then
                    '+ La cantidad de unidades a comprar (+) debe ser menor o igual a las que tiene el  fondo
                    If CDec(nUnitsChange) <= 0 Then
                        .ErrorMessage(sCodispl, 10291)
                    Else
                        lclsFunds.nUnitsPurchase = lclsFunds.nUnitsPurchase + CDec(nUnitsChange)

                        If nValueChange > (nAvailable + nValueChange_aux) Then
                            .ErrorMessage(sCodispl, 70120)
                        End If
                    End If
                End If

                ' Venta de unidades
            ElseIf nSignal = 2 Then
                '+ En caso de vender se efectua la misma validación, pero en el stock de fondos.
                If nUnits <= 0 Then
                    .ErrorMessage(sCodispl, 3761)
                ElseIf CDec(nUnitsChange) <= 0 Then
                    .ErrorMessage(sCodispl, 10291)
                    '+ En caso de efectuar una venta de unidades, el valor de
                    '+ las unidades no debe ser mayor que la actual de unidades
                ElseIf CDec(nUnitsChange) > CDec(nUnits) Then
                    .ErrorMessage(sCodispl, 3761)
                Else
                    If CDec(nUnitsChange) = CDec(nUnits) And sActivFound <> "1" And sWindowsType = "PopUp" Then
                        .ErrorMessage(sCodispl, 56206)
                    End If
                End If
                '+ si la operación es venta no deben existir abonos pendientes
                lclsFunds.nUnitsSales = lclsFunds.nUnitsSales + CDec(nUnitsChange)
                If lclsUl_Move_Acc_pol.FindUl_Move_Acc_pol_Api("2", nBranch, nProduct, nPolicy, nCertif, dEffecdate) Then
                    If lclsUl_Move_Acc_pol.sExiulmap = "1" Then
                        .ErrorMessage(sCodispl, 56207)
                    End If
                End If
            End If
            nErrorNum = ValProp_pend_upd(sCertype, nBranch, nProduct, nPolicy, nCertif, nFunds, nSignal)
            If nErrorNum > 0 Then
                .ErrorMessage(sCodispl, nErrorNum)
            End If

            insValVI016 = .Confirm
        End With

insValVI016_Err:
        If Err.Number Then
            insValVI016 = "insValVI016: " & Err.Description
        End If
        On Error GoTo 0
        lclsUl_Move_Acc_pol = Nothing
        lclsFunds = Nothing
        lobjErrors = Nothing
    End Function

    '**%Objective: This function is in charge of updating the data in the VI010 form (Folder)
    '**%Parameters:
    '%Objetivo: Esta función se encarga de actualizar los datos de la forma VI010 (Folder)
    '%Parámetros:
    Public Function insPostVI010(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFunds As Integer, ByVal nSignal As Integer, ByVal nUnitsChange As Double, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nSell_cost As Double, ByVal nBuy_cost As Double, ByVal nSwi_cost As Double, ByVal nValueChange As Double, ByVal nOrigin As Double, ByVal sProcessType As String) As Boolean
        Dim lrecinsPostVI010 As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lrecinsPostVI010 = New eRemoteDB.Execute

        With lrecinsPostVI010
            .StoredProcedure = "insPostVI010"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSignal", nSignal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnitsChange", nUnitsChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSell_cost", nSell_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBuy_cost", nBuy_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSwi_cost", nSwi_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValueChange", nValueChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcessType", sProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostVI010 = True
            Else
                insPostVI010 = False
            End If
        End With

ErrorHandler:
        If Err.Number Then
            insPostVI010 = False
        End If
        On Error GoTo 0
        lrecinsPostVI010 = Nothing
    End Function

    '**%Objective: This function is in charge of updating the data in the VI016 form (Folder)
    '**%Parameters:
    '%Objetivo: Esta función se encarga de actualizar los datos de la forma VI016 (Folder)
    '%Parámetros:
    Public Function insPostVI016(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFunds As Integer, ByVal nSignal As Integer, ByVal nUnitsChange As Double, ByVal nCurrency As Integer, ByVal nUsercode As Integer, ByVal nSell_cost As Double, ByVal nBuy_cost As Double, ByVal nSwi_cost As Double, ByVal nValueChange As Double, ByVal nOrigin As Double, ByVal sProcessType As String, ByVal nTyp_Profitworker As Integer) As Boolean
        Dim lrecinsPostVI016 As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lrecinsPostVI016 = New eRemoteDB.Execute

        With lrecinsPostVI016
            .StoredProcedure = "insPostVI016"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSignal", nSignal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUnitsChange", nUnitsChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSell_cost", nSell_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBuy_cost", nBuy_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSwi_cost", nSwi_cost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nValueChange", nValueChange, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcessType", sProcessType, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCommit", 1, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_Profitworker", nTyp_Profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostVI016 = True
            Else
                insPostVI016 = False
            End If
        End With

ErrorHandler:
        If Err.Number Then
            insPostVI016 = False
        End If
        On Error GoTo 0
        lrecinsPostVI016 = Nothing
    End Function

    '**%Objective: This function is in charge of updating the data in the VI010 form (Folder)
    '**%Parameters:
    '%Objetivo: Esta función se encarga de actualizar los datos de la forma VI010 (Folder)
    '%Parámetros:
    Public Function insPostVI010_A(ByVal sSel As String, ByVal sBranch As String, ByVal sProduct As String, ByVal sPolicy As String, ByVal sCertif As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecinsPostVI010_A As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lrecinsPostVI010_A = New eRemoteDB.Execute

        With lrecinsPostVI010_A
            .StoredProcedure = "insPostVI010_A"
            .Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBranch", sBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProduct", sProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolicy", sPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertif", sCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostVI010_A = True
            Else
                insPostVI010_A = False
            End If
        End With

ErrorHandler:
        If Err.Number Then
            insPostVI010_A = False
        End If
        On Error GoTo 0
        lrecinsPostVI010_A = Nothing
    End Function

    '**%Objective: This function is in charge of updating the data in the VI016 form (Folder)
    '**%Parameters:
    '%Objetivo: Esta función se encarga de actualizar los datos de la forma VI016 (Folder)
    '%Parámetros:
    Public Function insPostVI016_A(ByVal sSel As String, ByVal sBranch As String, ByVal sProduct As String, ByVal sPolicy As String, ByVal sCertif As String, ByVal dEffecdate As Date, ByVal nUsercode As Integer) As Boolean
        Dim lrecinsPostVI016_A As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lrecinsPostVI016_A = New eRemoteDB.Execute

        With lrecinsPostVI016_A
            .StoredProcedure = "insPostVI016_A"
            .Parameters.Add("sSel", sSel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sBranch", sBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProduct", sProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolicy", sPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sCertif", sCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                insPostVI016_A = True
            Else
                insPostVI016_A = False
            End If
        End With

ErrorHandler:
        If Err.Number Then
            insPostVI016_A = False
        End If
        On Error GoTo 0
        lrecinsPostVI016_A = Nothing
    End Function

    '%Objetivo: Permite verificar si una póliza tiene asociado fondos No Vigentes (Antiguos)
    Public Function ValFundsPol_NoVigen(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date) As Boolean
        Dim lrecValFundsPol_NoVigen_1 As eRemoteDB.Execute

        On Error GoTo ErrorHandler

        lrecValFundsPol_NoVigen_1 = New eRemoteDB.Execute

        With lrecValFundsPol_NoVigen_1
            .StoredProcedure = "ValFunds_Pol_NoVigen"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then
                ValFundsPol_NoVigen = True
                .RCloseRec()
            End If
        End With

        lrecValFundsPol_NoVigen_1 = Nothing

        Exit Function

ErrorHandler:
        lrecValFundsPol_NoVigen_1 = Nothing

        ValFundsPol_NoVigen = False
    End Function

    '%Objetivo: Permite verificar si una póliza tiene una propuesta de traspaso pendiente
    Public Function ValProp_pend(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Double
        Dim lrecValProp_pend As eRemoteDB.Execute
        Dim npolicy_prop As Double
        On Error GoTo ErrorHandler

        lrecValProp_pend = New eRemoteDB.Execute

        With lrecValProp_pend
            .StoredProcedure = "INSVI010pkg.Reaprop_vi010"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("npolicy_prop", npolicy_prop, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            ValProp_pend = .Parameters("npolicy_prop").Value
        End With

        lrecValProp_pend = Nothing

        Exit Function

ErrorHandler:
        lrecValProp_pend = Nothing

        ValProp_pend = 0
    End Function

    '%Objetivo: Permite verificar si una póliza tiene una propuesta con un movimiento inverso asociado compra/venta
    Public Function ValProp_pend_upd(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal nFunds As Integer, ByVal nSignal As Integer) As Double
        Dim lrecValProp_pend As eRemoteDB.Execute
        Dim nError As Double
        On Error GoTo ErrorHandler

        lrecValProp_pend = New eRemoteDB.Execute

        With lrecValProp_pend
            .StoredProcedure = "INSVI010pkg.Reaprop_vi010_upd"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSignal", nSignal, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nError", nError, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            ValProp_pend_upd = .Parameters("nError").Value
        End With

        lrecValProp_pend = Nothing

        Exit Function

ErrorHandler:
        lrecValProp_pend = Nothing

        ValProp_pend_upd = 0
    End Function

    '%Objetivo: Permite eliminar una propuesta de traspaso pendiente
    Public Function DelProp_pend(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double) As Boolean
        Dim lrecDelProp_pend As eRemoteDB.Execute
        Dim npolicy_prop As Double
        On Error GoTo ErrorHandler

        lrecDelProp_pend = New eRemoteDB.Execute

        With lrecDelProp_pend
            .StoredProcedure = "INSVI010pkg.Delprop_vi010"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            DelProp_pend = .Run(False)
        End With

        lrecDelProp_pend = Nothing

        Exit Function

ErrorHandler:
        lrecDelProp_pend = Nothing
        DelProp_pend = False
    End Function



    '**%Objective: This method obtains the information from the table '<__TABLE__>'
    '**%Parameters:
    '**%    sCertype - .
    '**%    nBranch - .
    '**%    nProduct - .
    '**%    nPolicy - .
    '**%    nCertif - .
    '%Objetivo: Este método realiza la lectura de la información de la tabla en tratamiento '<__TABLE__>'.
    '%Parámetros:
    '%    sCertype - .
    '%    nBranch - .
    '%    nProduct - .
    '%    nPolicy - .
    '%    nCertif - .
    Public Function ReaBalance_Difpol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Integer, ByVal nCertif As Integer, ByVal sCodispl As String) As Boolean
        Dim lrecreabalance_difpol As eRemoteDB.Execute
        Dim lclsErrors As eFunctions.Errors
        Dim lstrErrorAll As String = ""

        On Error GoTo ReaBalance_Difpol_Err

        lrecreabalance_difpol = New eRemoteDB.Execute
        With lrecreabalance_difpol
            .StoredProcedure = "reabalance_difpol"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sArrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("sArrayerrors").Value
            End If
        End With
        lrecreabalance_difpol = Nothing

        lclsErrors = New eFunctions.Errors
        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            ReaBalance_Difpol = CBool(.Confirm)
        End With
        lclsErrors = Nothing

ReaBalance_Difpol_Err:
        If Err.Number Then
            ReaBalance_Difpol = CBool("ReaBalance_Difpol: " & Err.Description)
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function

    '**%Objective: Reads all the active funds associated to a policy
    '%Objetivo: Lee todos los fondos activos asociados a una póliza
    Public Function Delete_Surr_Origins_Funds(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nProponum As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nOrigin As Integer, ByVal nTyp_Profitworker As Integer) As Boolean
        Dim lrecdelSurr_Origins_Funds As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecdelSurr_Origins_Funds = New eRemoteDB.Execute

        Delete_Surr_Origins_Funds = True

        With lrecdelSurr_Origins_Funds
            .StoredProcedure = "delSurr_Origins_Funds"

            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProponum", nProponum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_Profitworker", nTyp_Profitworker, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            Delete_Surr_Origins_Funds = .Run(False)
        End With

        lrecdelSurr_Origins_Funds = Nothing

        Exit Function
ErrorHandler:
        lrecdelSurr_Origins_Funds = Nothing

        Delete_Surr_Origins_Funds = False
    End Function

    '**%Objective: Reads all actives funds related to the policy
    '%Objetivo: Lee todos los fondos activos asociados a una póliza
    Public Function Find_Funds_pol(ByVal sCertype As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nPolicy As Double, ByVal nCertif As Double, ByVal dEffecdate As Date, ByVal nFunds As Integer, ByVal nOrigin As Integer, ByVal nOption As Double) As Boolean
        Dim lrecreaFunds_pol As eRemoteDB.Execute

        On Error GoTo ErrorHandler
        lrecreaFunds_pol = New eRemoteDB.Execute

        Find_Funds_pol = True

        With lrecreaFunds_pol
            .StoredProcedure = "reaFunds_pol_4"
            .Parameters.Add("sCertype", sCertype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy", nPolicy, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCertif", nCertif, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOption", nOption, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Find_Funds_pol = .Run
            If Find_Funds_pol Then
                dNulldate = .FieldToClass("dNulldate")
                nParticip = .FieldToClass("nParticip")
                sDescript = .FieldToClass("sDescript")
                sReaddress = .FieldToClass("sReaddress")
                nQuan_avail = .FieldToClass("nQuan_avail")
                .RCloseRec()
            End If
        End With

        lrecreaFunds_pol = Nothing

        Exit Function
ErrorHandler:
        lrecreaFunds_pol = Nothing

        Find_Funds_pol = False
    End Function
End Class






