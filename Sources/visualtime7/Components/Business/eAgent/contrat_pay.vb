Option Strict Off
Option Explicit On
<System.Runtime.InteropServices.ProgId("Contrat_Pay_NET.Contrat_Pay")> Public Class Contrat_Pay
	'%-------------------------------------------------------%'
	'% $Workfile:: contrat_pay.cls                           $%'
	'% $Author:: Nvaplat7                                   $%'
	'% $Date:: 9/08/03 1:38p                                $%'
	'% $Revision:: 12                                       $%'
	'%-------------------------------------------------------%'
	
	'-
	'- Estructura de tabla contrat_pay al 09-19-2002 14:02:13
	'-     Property                Type
	'--------------------------------------------
	Public nContrat_Pay As Integer
	Public sClient As String
	Public sDescript As String
	Public dStartdate As Date
	Public nType_Calc As Integer
	Public nPercent As Double
	Public nAmount As Double
	Public nCurrency As Integer
	Public nAply As Integer
	Public sTaxin As String
    Public sStatregt As String
    Public nTyp_acco As Integer

    Private mlngUsercode As Integer
    Public blnValues As Boolean

    Public nBranch As Integer
    Public nProduct As Integer

    Public dEffecdate As Date
    Public dNulldate As Date
    Public dCompdate As Date

    Public NAMOUNT_INI As Integer
    Public SROUTINE As String
    Public NTYPE_CONTRAT As Integer
    Public NMODULEC As Integer
    Public NPOLICY_DUR As Integer
    Public NAGE_INIT As Integer
    Public NAGE_END As Integer

    Public nUsercode As Integer


    Public mcolContrat_Pay_Detail As Contrat_Pay_Details

    Public mcolContrat_Pay_Prod As Contrat_Pays

    Private Property nAction As Integer

    Private Property sAction As String

    '%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
    '%tabla "Contrat_Pay"
    Public Function Find(ByVal nContrat_Pay As Integer) As Boolean
        Dim lrecreaContrat_Pay As eRemoteDB.Execute

        On Error GoTo Find_Err

        '+Definición de parámetros para stored procedure 'insudb.reaContrat_Pay'
        '+Información leída el 22/01/2001 2:59:05 PM
        lrecreaContrat_Pay = New eRemoteDB.Execute
        With lrecreaContrat_Pay
            .StoredProcedure = "reaContrat_Pay"
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sClient = .FieldToClass("sClient")
                sDescript = .FieldToClass("sDescript")
                dStartdate = .FieldToClass("dStartDate")
                dEffecdate = .FieldToClass("dEffecdate")
                nType_Calc = .FieldToClass("nType_Calc")
                nPercent = .FieldToClass("nPercent")
                nAmount = .FieldToClass("nAmount")
                nCurrency = .FieldToClass("nCurrency")
                nAply = .FieldToClass("nAply")
                sTaxin = .FieldToClass("sTaxin")
                sStatregt = .FieldToClass("sStatregt")
                nTyp_acco = .FieldToClass("nTyp_acco")
                .RCloseRec()
                Find = True
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If
        'UPGRADE_NOTE: Object lrecreaContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContrat_Pay = Nothing
        On Error GoTo 0
    End Function

    Public Function Find_ContratPayProd(ByVal nContrat_Pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        Dim lrecreaContrat_Pay As eRemoteDB.Execute

        On Error GoTo Find_Err

        '+Definición de parámetros para stored procedure 'insudb.REACONTRAT_PAY_PROD'
        '+Información leída el 09/05/2014 2:59:05 PM
        lrecreaContrat_Pay = New eRemoteDB.Execute
        With lrecreaContrat_Pay
            .StoredProcedure = "REACONTRAT_PAY_PROD"
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                sClient = .FieldToClass("sClient")
                sDescript = .FieldToClass("sDescript")
                dStartdate = .FieldToClass("dStartDate")
                dEffecdate = .FieldToClass("dEffecdate")
                nType_Calc = .FieldToClass("nType_Calc")
                nPercent = .FieldToClass("nPercent")
                nAmount = .FieldToClass("nAmount")
                nCurrency = .FieldToClass("nCurrency")
                nAply = .FieldToClass("nAply")
                sTaxin = .FieldToClass("sTaxin")
                sStatregt = .FieldToClass("sStatregt")
                nTyp_acco = .FieldToClass("nTyp_acco")

                NMODULEC = .FieldToClass("nModulec")
                NPOLICY_DUR = .FieldToClass("nPolicy_Dur")
                NAGE_INIT = .FieldToClass("nAge_Init")
                NAGE_END = .FieldToClass("nAge_End")
                'DEFFECDATE, DNULLDATE, NMODULEC, NPOLICY_DUR, NAGE_INIT, NAGE_END
                .RCloseRec()
                Find_ContratPayProd = True
            End If
        End With

Find_Err:
        If Err.Number Then
            Find_ContratPayProd = False
        End If
        'UPGRADE_NOTE: Object lrecreaContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContrat_Pay = Nothing
        On Error GoTo 0
    End Function


    '%InsUpdContrat_Pay: Actualiza la informacion de la tabla de vehiculos
    Private Function InsUpdContrat_Pay(ByVal nAction As Integer) As Boolean
        Dim lrecinsUpdContrat_Pay As eRemoteDB.Execute

        On Error GoTo insUpdContrat_Pay_Err
        lrecinsUpdContrat_Pay = New eRemoteDB.Execute
        '+ Definición de store procedure insUpdContrat_Pay al 10-03-2002 15:57:37
        With lrecinsUpdContrat_Pay
            .StoredProcedure = "insUpdContrat_Pay"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_Calc", nType_Calc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAply", nAply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTaxin", sTaxin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarChar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsUpdContrat_Pay = .Run(False)
        End With

insUpdContrat_Pay_Err:
        If Err.Number Then
            InsUpdContrat_Pay = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdContrat_Pay = Nothing
        On Error GoTo 0
    End Function

    Private Function InsUpdContrat_Pay_Prod(ByVal nAction As Integer) As Boolean
        Dim lrecinsUpdContrat_Pay_Prod As eRemoteDB.Execute

        On Error GoTo insUpdContrat_Pay_Prod_Err
        lrecinsUpdContrat_Pay_Prod = New eRemoteDB.Execute
        '+ Definición de store procedure insUpdContrat_Pay al 10-03-2002 15:57:37
        With lrecinsUpdContrat_Pay_Prod
            .StoredProcedure = "insUpdContrat_Pay_Prod"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 60, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStartDate", dStartdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_Calc", nType_Calc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 9, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAply", nAply, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sTaxin", sTaxin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sStatregt", sStatregt, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount_Ini", NAMOUNT_INI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sRoutine", SROUTINE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 12, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nType_Contrat", NTYPE_CONTRAT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nModulec", NMODULEC, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPolicy_Dur", NPOLICY_DUR, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_Init", NAGE_INIT, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAge_End", NAGE_END, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            InsUpdContrat_Pay_Prod = .Run(False)
        End With

insUpdContrat_Pay_Prod_Err:
        If Err.Number Then
            InsUpdContrat_Pay_Prod = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdContrat_Pay_Prod = Nothing
        On Error GoTo 0
    End Function

    '%Add: Esta función agrega registros a la tabla Contrat_Pay
    Public Function Add() As Boolean
        Add = InsUpdContrat_Pay(1)
    End Function

    '%Update: Esta función actualiza registros en la tabla Contrat_Pay
    Public Function Update() As Boolean
        Update = InsUpdContrat_Pay(2)
    End Function

    '%Delete: Esta función elimina registros de la tabla Contrat_Pay
    Public Function Delete() As Boolean
        Delete = InsUpdContrat_Pay(3)
    End Function

    '%IsExist: Valida la existencia de un código.
    Public Function IsExist(ByVal nContrat_Pay As Integer) As Boolean
        Dim lrecContrat_Pay As eRemoteDB.Execute

        On Error GoTo IsExist_Err
        lrecContrat_Pay = New eRemoteDB.Execute
        With lrecContrat_Pay
            .StoredProcedure = "valContrat_Pay"
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            IsExist = .Parameters("nCount").Value > 0
        End With

IsExist_Err:
        If Err.Number Then
            IsExist = False
        End If
        'UPGRADE_NOTE: Object lrecContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecContrat_Pay = Nothing
        On Error GoTo 0
    End Function

    '%IsExistProd: Valida la existencia de un código.
    Public Function IsExistProd(ByVal nContrat_Pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        Dim lrecContrat_Pay As eRemoteDB.Execute

        On Error GoTo IsExistProd_Err
        lrecContrat_Pay = New eRemoteDB.Execute
        With lrecContrat_Pay
            .StoredProcedure = "valContrat_Pay_Prod"
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            IsExistProd = .Parameters("nCount").Value > 0
        End With

IsExistProd_Err:
        If Err.Number Then
            IsExistProd = False
        End If
        'UPGRADE_NOTE: Object lrecContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecContrat_Pay = Nothing
        On Error GoTo 0
    End Function


    '%InsValAG954_K: Esta función se encarga de validar los datos introducidos en la cabecera de
    '%la forma.
    Public Function InsValAG954_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nContrat_Pay As Integer) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lblnExist As Boolean

        On Error GoTo InsValAG954_K_Err
        lclsErrors = New eFunctions.Errors

        With lclsErrors
            '+Validación del campo: Código.
            If nContrat_Pay = eRemoteDB.Constants.intNull Or nContrat_Pay = 0 Then
                Call .ErrorMessage(sCodispl, 1012, , , ": Contrato")
            Else
                '+ Si la acción es registrar no debe existir información en la tabla.
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++
                '+ Crear funcion IsExist agregando nbranch y nproduct
                '+++++++++++++++++++++++++++++++++++++++++++++++++++++
                lblnExist = IsExist(nContrat_Pay)
                If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                    If lblnExist Then
                        .ErrorMessage(sCodispl, 21001)
                    End If
                    '+ Si la acción no es registrar se verifica que exista información en la tabla.
                Else
                    If Not lblnExist Then
                        .ErrorMessage(sCodispl, 21002)
                    End If
                End If
            End If
            InsValAG954_K = .Confirm
        End With

InsValAG954_K_Err:
        If Err.Number Then
            InsValAG954_K = "InsValAG954_K: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '%InsPreAG954: Esta función se encarga de validar los datos introducidos en la zona de detalle
    Public Function InsPreAG954(ByVal nContrat_Pay As Integer) As Boolean
        On Error GoTo InsPreAG954_Err

        '+Se busca los datos generales del vehículo
        If Find(nContrat_Pay) Then
            InsPreAG954 = True
            '+Se la información de la tabla de valores asegurados de vehículos.
            mcolContrat_Pay_Detail = New Contrat_Pay_Details
            Call mcolContrat_Pay_Detail.Find(CStr(nContrat_Pay))
        End If

        blnValues = InsPreAG954

InsPreAG954_Err:
        If Err.Number Then
            InsPreAG954 = False
        End If
        On Error GoTo 0
    End Function

    '%InsPreAG955: Esta función se encarga de validar los datos introducidos en la zona de detalle
    Public Function InsPreAG955(ByVal nContrat_Pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        On Error GoTo InsPreAG955_Err

        '+Se busca los datos generales del Estipendio 
        If Find_ContratPayProd(nContrat_Pay, nBranch, nProduct) Then
            InsPreAG955 = True
            '+Se la información de la tabla de valores de contratos de estipendio por producto
            mcolContrat_Pay_Prod = New Contrat_Pays
            Call mcolContrat_Pay_Prod.Find(nContrat_Pay, nBranch, nProduct)
        End If

        InsPreAG955 = True

        blnValues = InsPreAG955

InsPreAG955_Err:
        If Err.Number Then
            InsPreAG955 = False
        End If
        On Error GoTo 0
    End Function

    '%InsValAG954: Esta función se encarga de validar los datos introducidos en la zona de detalle
    Public Function InsValAG954(ByVal sCodispl As String, ByVal nContrat_Pay As Integer, ByVal sClient As String, ByVal sDescript As String, ByVal dStartdate As Date, ByVal nType_Calc As Integer, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nAply As Integer, ByVal sTaxin As String, ByVal sStatregt As String, ByVal nAction As Integer, ByVal nTyp_acco As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValAG954_Err
        lclsErrors = New eFunctions.Errors

        If nAction <> eFunctions.Menues.TypeActions.clngActioncut Then
            With lclsErrors
                '+ Se valida la columna: nVehbrand
                'If sClient = String.Empty Then
                '.ErrorMessage(sCodispl, 2001)
                'End If

                If sDescript = String.Empty Then
                    .ErrorMessage(sCodispl, 10071)
                End If

                If dStartdate = dtmNull Then
                    .ErrorMessage(sCodispl, 7114)
                End If

                If nTyp_acco = eRemoteDB.Constants.intNull Or nTyp_acco = 0 Then
                    .ErrorMessage(sCodispl, 7107)
                End If

                If nType_Calc = eRemoteDB.Constants.intNull Or nType_Calc = 0 Then
                    Call .ErrorMessage(sCodispl, 1012, , , ": Tipo de cálculo")
                Else
                    If nType_Calc = 1 Then 'Porcentaje fijo
                        If nPercent = eRemoteDB.Constants.intNull Or nPercent = 0 Then
                            .ErrorMessage(sCodispl, 55540)
                        End If
                        If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
                            .ErrorMessage(sCodispl, 750024)
                        End If
                        If nAmount <> eRemoteDB.Constants.intNull Then
                            .ErrorMessage(sCodispl, 100123)
                        End If
                    End If
                    If nType_Calc = 2 Then 'Monto fijo
                        If nAmount = eRemoteDB.Constants.intNull Or nAmount = 0 Then
                            Call .ErrorMessage(sCodispl, 1012, , , ": Monto")
                        End If
                        If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
                            .ErrorMessage(sCodispl, 750024)
                        End If
                        If nPercent <> eRemoteDB.Constants.intNull Then
                            .ErrorMessage(sCodispl, 100124)
                        End If
                    End If
                    If nType_Calc = 3 Then 'Según tabla
                        If nCurrency <> eRemoteDB.Constants.intNull Then
                            .ErrorMessage(sCodispl, 11417)
                        End If
                        If nAmount <> eRemoteDB.Constants.intNull Then
                            .ErrorMessage(sCodispl, 100123)
                        End If
                        If nPercent <> eRemoteDB.Constants.intNull Then
                            .ErrorMessage(sCodispl, 100124)
                        End If
                    End If
                    If nType_Calc = 4 Then 'Según Metas
                        If nCurrency <> eRemoteDB.Constants.intNull Then
                            .ErrorMessage(sCodispl, 11417)
                        End If
                        If nAmount <> eRemoteDB.Constants.intNull Then
                            .ErrorMessage(sCodispl, 100123)
                        End If
                        If nPercent <> eRemoteDB.Constants.intNull Then
                            .ErrorMessage(sCodispl, 100124)
                        End If
                    End If
                End If
                If nAply = eRemoteDB.Constants.intNull Or nAply = 0 Then
                    Call .ErrorMessage(sCodispl, 1012, , , ": Elemento a aplicar")
                End If

                If nType_Calc = 1 And nAply <> 1 And nAply <> 2 Then
                    .ErrorMessage(sCodispl, 100131)
                End If

                If nType_Calc = 2 And nAply <> 3 And nAply <> 4 Then
                    .ErrorMessage(sCodispl, 100130)
                End If

                If nType_Calc = 3 And nAply <> 1 And nAply <> 2 Then
                    .ErrorMessage(sCodispl, 100132)
                End If

                If nType_Calc = 4 And nAply <> 5 Then
                    .ErrorMessage(sCodispl, 100134)
                End If

                If sTaxin = String.Empty Then
                    sTaxin = "2"
                End If
                If sStatregt = String.Empty Then
                    .ErrorMessage(sCodispl, 9089)
                End If
                InsValAG954 = .Confirm
            End With
        End If

InsValAG954_Err:
        If Err.Number Then
            InsValAG954 = "InsValAG954: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '%InsValAG955: Esta función se encarga de validar los datos introducidos en la zona de detalle
    Public Function InsValAG955(ByVal sCodispl As String, ByVal nContrat_Pay As Integer, ByVal nType_Calc As Short, ByVal sClient As String, ByVal dStartdate As Date, ByVal nPercent As Double, ByVal nModulec As Integer, ByVal nAge_Init As Short, ByVal nAge_End As Short, ByVal nPolicy_Dur As Short, ByVal nAction As Integer) As String
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo InsValAG955_Err
        lclsErrors = New eFunctions.Errors

        If nAction <> eFunctions.Menues.TypeActions.clngActioncut Then
            With lclsErrors
                '+ Se valida la columna: nVehbrand
                'If sClient = String.Empty Then
                '.ErrorMessage(sCodispl, 2001)
                'End If

                If dStartdate = dtmNull Then
                    .ErrorMessage(sCodispl, 7114)
                End If

                If nType_Calc = 2 Then 'Monto fijo
                    If nAmount = eRemoteDB.Constants.intNull Or nAmount = 0 Then
                        Call .ErrorMessage(sCodispl, 1012, , , ": Monto")
                    End If
                    If nCurrency = eRemoteDB.Constants.intNull Or nCurrency = 0 Then
                        .ErrorMessage(sCodispl, 750024)
                    End If
                    If nPercent <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 100124)
                    End If
                End If
                If nType_Calc = 3 Then 'Según tabla
                    If nCurrency <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 11417)
                    End If
                    If nAmount <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 100123)
                    End If
                    If nPercent <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 100124)
                    End If
                End If
                If nType_Calc = 4 Then 'Según Metas
                    If nCurrency <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 11417)
                    End If
                    If nAmount <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 100123)
                    End If
                    If nPercent <> eRemoteDB.Constants.intNull Then
                        .ErrorMessage(sCodispl, 100124)
                    End If
                End If

                If nType_Calc = 1 And nAply <> 1 And nAply <> 2 Then
                    .ErrorMessage(sCodispl, 100131)
                End If

                If nType_Calc = 2 And nAply <> 3 And nAply <> 4 Then
                    .ErrorMessage(sCodispl, 100130)
                End If

                If nType_Calc = 3 And nAply <> 1 And nAply <> 2 Then
                    .ErrorMessage(sCodispl, 100132)
                End If

                If nType_Calc = 4 And nAply <> 5 Then
                    .ErrorMessage(sCodispl, 100134)
                End If
                InsValAG955 = .Confirm
            End With
        End If

InsValAG955_Err:
        If Err.Number Then
            InsValAG955 = "InsValAG955: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

    '%InsPostMAU001: Esta función se encarga de crear/actualizar los registros
    '%               correspondientes en la tabla Tab_au_veh
    Public Function InsPostAG954(ByVal nAction As Integer, ByVal nContrat_Pay As Integer, ByVal sClient As String, ByVal sDescript As String, ByVal dStartdate As Date, ByVal nType_Calc As Integer, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nAply As Integer, ByVal sTaxin As String, ByVal sStatregt As String, ByVal nUsercode As Integer, ByVal nTyp_acco As Integer) As Boolean

        On Error GoTo InsPostag954_err

        With Me
            InsPostAG954 = True
            If nAction <> eFunctions.Menues.TypeActions.clngActionQuery Then

                .nContrat_Pay = nContrat_Pay
                .sClient = sClient
                .sDescript = sDescript
                .dStartdate = dStartdate
                .nType_Calc = nType_Calc
                .nPercent = nPercent
                .nAmount = nAmount
                .nCurrency = nCurrency
                .nAply = nAply
                .sTaxin = sTaxin
                .sStatregt = sStatregt
                mlngUsercode = nUsercode
                .nTyp_acco = nTyp_acco

                Select Case nAction
                    '+Si la opción seleccionada es Registrar
                    Case eFunctions.Menues.TypeActions.clngActionadd
                        InsPostAG954 = .Add()

                        '+Si la opción seleccionada es Modificar
                    Case eFunctions.Menues.TypeActions.clngActionUpdate
                        InsPostAG954 = .Update()

                        '+Si la opción seleccionada es Eliminar
                    Case eFunctions.Menues.TypeActions.clngActioncut
                        InsPostAG954 = .Delete()
                End Select
            End If
        End With

InsPostag954_err:
        If Err.Number Then
            InsPostAG954 = False
        End If
        On Error GoTo 0
    End Function

    '%InsValAG954Upd: Valida las partes repetitivas de la transacción
    Public Function InsValAG954Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nContrat_Pay As Integer, ByVal sClient As String, ByVal sDescript As String, ByVal dStartdate As Date, ByVal nType_Calc As Integer, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nAply As Integer, ByVal sTaxin As String, ByVal sStatregt As String, ByVal nSeq As Double, ByVal nCode As Integer, ByVal nInit_Dur As Integer, ByVal nEnd_Dur As Integer, ByVal nPercent_detail As Double) As String
        Dim lclsObject As Object
        On Error GoTo InsValAG954Upd_Err

        lclsObject = New contrat_pay_detail
        InsValAG954Upd = lclsObject.InsValAG954Upd_Detail(sCodispl, sAction, nContrat_Pay, sClient, sDescript, dStartdate, nType_Calc, nPercent, nAmount, nCurrency, nAply, sTaxin, sStatregt, nSeq, nCode, nInit_Dur, nEnd_Dur, nPercent_detail)

InsValAG954Upd_Err:
        If Err.Number Then
            InsValAG954Upd = "InsValAG954Upd: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsObject = Nothing
        On Error GoTo 0
    End Function

    '%InsUpdTab_au_val: Actualiza la informacion de la tabla
    Private Function InsUpdContrat_Pay_Prod_Detail(ByVal nAction As Integer) As Boolean
        Dim lrecinsUpdContrat_Pay_Prod_Detail As eRemoteDB.Execute

        On Error GoTo InsUpdContrat_Pay_Prod_Detail_Err
        lrecinsUpdContrat_Pay_Prod_Detail = New eRemoteDB.Execute
        '+ Definición de store procedure insUpdtab_au_val al 10-03-2002 16:40:43
        With lrecinsUpdContrat_Pay_Prod_Detail
            .StoredProcedure = "insUpdContrat_Pay_Prod_Detail"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            '  .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.Date, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", mlngUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsUpdContrat_Pay_Prod_Detail = .Run(False)
        End With

insUpdContrat_Pay_Prod_Detail_Err:
        If Err.Number Then
            InsUpdContrat_Pay_Prod_Detail = False
        End If
        'UPGRADE_NOTE: Object lrecinsUpdContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsUpdContrat_Pay_Prod_Detail = Nothing
        On Error GoTo 0
    End Function

    '%IsExist: Valida la existencia de un código.
    Public Function IsExist_contrat(ByVal nContrat_Pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        Dim lrecContrat_Pay As eRemoteDB.Execute

        On Error GoTo IsExist_Err
        lrecContrat_Pay = New eRemoteDB.Execute
        With lrecContrat_Pay
            .StoredProcedure = "valContrat_Pay_Prod"
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCount", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            IsExist_contrat = .Parameters("nCount").Value > 0
        End With

IsExist_Err:
        If Err.Number Then
            IsExist_contrat = False
        End If
        'UPGRADE_NOTE: Object lrecContrat_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecContrat_Pay = Nothing
        On Error GoTo 0
    End Function

    '%Add: Esta función agrega registros a la tabla contrat_pay_prod
    Public Function Add_Detail() As Boolean
        If Not IsExist_contrat(nContrat_Pay, nBranch, nProduct) Then
            If InsUpdContrat_Pay(1) Then
                Add_Detail = InsUpdContrat_Pay_Prod(1)
            End If
        Else
            If InsUpdContrat_Pay(2) Then
                Add_Detail = InsUpdContrat_Pay_Prod(1)
            End If
        End If
    End Function

    '%Update: Esta función actualiza registros en la tabla TAB_AU_VAL
    Public Function Update_Detail() As Boolean

        If InsUpdContrat_Pay(2) Then
            Update_Detail = InsUpdContrat_Pay_Prod(2)
        End If

    End Function

    '%Delete: Esta función elimina registros de la tabla TAB_AU_VAL
    Public Function Delete_Detail() As Boolean
        Delete_Detail = InsUpdContrat_Pay_Prod(3)
    End Function

    '%InsPostMAU001Upd: Valida las partes repetitivas de la transacción
    Public Function InsPostAG954Upd(ByVal sAction As String, ByVal nContrat_Pay As Integer, ByVal sClient As String, ByVal sDescript As String, ByVal dStartdate As Date, ByVal nType_Calc As Integer, ByVal nPercent As Double, ByVal nAmount As Double, ByVal nCurrency As Integer, ByVal nAply As Integer, ByVal sTaxin As String, ByVal sStatregt As String, ByVal nSeq As Double, ByVal nCode As Integer, ByVal nInit_Dur As Integer, ByVal nEnd_Dur As Integer, ByVal nPercent_detail As Double, ByVal nUsercode As Object) As Boolean
        Dim lclsObject As Object
        On Error GoTo InsPostAG954Upd_Err

        lclsObject = New contrat_pay_detail
        InsPostAG954Upd = lclsObject.InsPostAG954Upd_detail(sAction, nContrat_Pay, sClient, sDescript, dStartdate, nType_Calc, nPercent, nAmount, nCurrency, nAply, sTaxin, sStatregt, nSeq, nCode, nInit_Dur, nEnd_Dur, nPercent_detail, nUsercode)

InsPostAG954Upd_Err:
        If Err.Number Then
            InsPostAG954Upd = False
        End If
        'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsObject = Nothing
        On Error GoTo 0
    End Function

    '%InsPostAG955Upd: Valida las partes repetitivas de la transacción
    Public Function InsPostAG955Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nContrat_Pay As Integer, ByVal dEffecdate As Date, ByVal dNulldate As Date, ByVal sClient As String, ByVal sDescript As String, ByVal dStartDate As Date, ByVal nType_Calc As Integer, ByVal nPercent As Double, ByVal nAmount As Integer, ByVal nCurrency As Integer, ByVal nAply As Integer, ByVal sTaxin As String, ByVal sStatregt As String, ByVal nUsercode As Object, ByVal nTyp_Acco As Integer, ByVal nAmount_Ini As Integer, ByVal sRoutine As String, ByVal nType_Contrat As Integer, ByVal nModulec As Integer, ByVal nPolicy_Dur As Integer, ByVal nAge_Init As Integer, ByVal nAge_End As Integer) As Boolean
        On Error GoTo InsPostAG955Upd_Err

        With Me
            .sAction = sAction
            .nBranch = nBranch
            .nProduct = nProduct
            .nContrat_Pay = nContrat_Pay
            .dEffecdate = dEffecdate
            .dNulldate = dNulldate
            .sClient = sClient
            .sDescript = sDescript
            .dStartdate = dStartDate
            .nType_Calc = nType_Calc
            .nPercent = nPercent
            .nAmount = nAmount
            .nCurrency = nCurrency
            .nAply = nAply
            .sTaxin = sTaxin
            .sStatregt = sStatregt
            mlngUsercode = nUsercode
            .nTyp_acco = nTyp_Acco
            .NAMOUNT_INI = nAmount_Ini
            .SROUTINE = sRoutine
            .NTYPE_CONTRAT = nType_Contrat
            .NMODULEC = nModulec
            .NPOLICY_DUR = nPolicy_Dur
            .NAGE_INIT = nAge_Init
            .NAGE_END = NAGE_END


            Select Case sAction
                '+Si la opción seleccionada es Registrar
                Case "Add"
                    InsPostAG955Upd = InsUpdContrat_Pay_Prod(1)

                    '+Si la opción seleccionada es Modificar
                Case "Update"
                    InsPostAG955Upd = InsUpdContrat_Pay_Prod(2)

                    '+Si la opción seleccionada es Eliminar
                Case "Del"
                    InsPostAG955Upd = InsUpdContrat_Pay_Prod(3)
            End Select
        End With

InsPostAG955Upd_Err:
        If Err.Number Then
            InsPostAG955Upd = False
        End If
        'UPGRADE_NOTE: Object lclsObject may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        On Error GoTo 0
    End Function

    '%Class_Initialize: Se ejecuta cuando se instancia la clase
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        nContrat_Pay = eRemoteDB.Constants.intNull
        sClient = String.Empty
        sDescript = String.Empty
        dStartdate = dtmNull
        nType_Calc = eRemoteDB.Constants.intNull
        nPercent = eRemoteDB.Constants.intNull
        nAmount = eRemoteDB.Constants.intNull
        nCurrency = eRemoteDB.Constants.intNull
        nAply = eRemoteDB.Constants.intNull
        sTaxin = String.Empty
        sStatregt = String.Empty
        mlngUsercode = eRemoteDB.Constants.intNull
        nTyp_acco = eRemoteDB.Constants.intNull
    End Sub
	Public Sub New()
		MyBase.New()
		Class_Initialize_Renamed()
	End Sub
	
	'%Class_Terminate: Se ejecuta cuando se destruye la clase
	'UPGRADE_NOTE: Class_Terminate was upgraded to Class_Terminate_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub Class_Terminate_Renamed()
		'UPGRADE_NOTE: Object mcolContrat_Pay_Detail may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		mcolContrat_Pay_Detail = Nothing
	End Sub
	Protected Overrides Sub Finalize()
		Class_Terminate_Renamed()
		MyBase.Finalize()
    End Sub


    '%Find: Este metodo retorna VERDADERO o FALSO dependiendo de la existencia o no de registros en la
    '%tabla "Contrat_Pay"
    Public Function Find_Prod(ByVal nContrat_Pay As Integer, ByVal nBranch As Integer, ByVal nProduct As Integer) As Boolean
        Dim lrecreaContrat_Pay_prod As eRemoteDB.Execute

        On Error GoTo Find_Prod_Err

        '+Definición de parámetros para stored procedure 'insudb.reaContrat_Pay_Prod'
        '+Información leída el 22/01/2001 2:59:05 PM
        lrecreaContrat_Pay_prod = New eRemoteDB.Execute
        With lrecreaContrat_Pay_prod
            .StoredProcedure = "REACONTRAT_PAY_PROD"
            .Parameters.Add("nContrat_Pay", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nContrat_Pay, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                sClient = .FieldToClass("sClient")
                sDescript = .FieldToClass("sDescript")
                dStartdate = .FieldToClass("dStartDate")
                dEffecdate = .FieldToClass("dEffecdate")
                nType_Calc = .FieldToClass("nType_Calc")
                nPercent = .FieldToClass("nPercent")
                nAmount = .FieldToClass("nAmount")
                nCurrency = .FieldToClass("nCurrency")
                nAply = .FieldToClass("nAply")
                sTaxin = .FieldToClass("sTaxin")
                sStatregt = .FieldToClass("sStatregt")
                nTyp_acco = .FieldToClass("nTyp_acco")
                .RCloseRec()
                Find_Prod = True
            End If
        End With

Find_Prod_Err:
        If Err.Number Then
            Find_Prod = False
        End If
        'UPGRADE_NOTE: Object lrecreaContrat_Pay_prod may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaContrat_Pay_prod = Nothing
        On Error GoTo 0
    End Function


    '%InsValAG954_K: Esta función se encarga de validar los datos introducidos en la cabecera de
    '%la forma.
    Public Function InsValAG955_K(ByVal sCodispl As String, ByVal nAction As Integer, ByVal nContrat_Pay As Integer, ByVal nBranch As Long, ByVal nProduct As Long) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lblnExist As Boolean

        On Error GoTo InsValAG955_K_Err
        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If nProduct = eRemoteDB.Constants.intNull Or nProduct = 0 Then
                Call .ErrorMessage(sCodispl, 1012, , , ": Producto")
            End If

            If nBranch = eRemoteDB.Constants.intNull Or nBranch = 0 Then
                Call .ErrorMessage(sCodispl, 1012, , , ": Ramo")
            End If

            '+Validación del campo: Código.
            If nContrat_Pay = eRemoteDB.Constants.intNull Or nContrat_Pay = 0 Then
                Call .ErrorMessage(sCodispl, 1012, , , ": Contrato")
            Else
                '+ Si la acción es registrar no debe existir información en la tabla.
                lblnExist = IsExistProd(nContrat_Pay, nBranch, nProduct)
                If nAction = eFunctions.Menues.TypeActions.clngActionadd Then
                    If lblnExist Then
                        .ErrorMessage(sCodispl, 21001)
                    End If

                    '+ Si la acción no es registrar se verifica que exista información en la tabla.
                Else
                    If Not lblnExist Then
                        .ErrorMessage(sCodispl, 21002)
                    End If
                End If
            End If
            InsValAG955_K = .Confirm
        End With

InsValAG955_K_Err:
        If Err.Number Then
            InsValAG955_K = "InsValAG955_K: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        On Error GoTo 0
    End Function

End Class






