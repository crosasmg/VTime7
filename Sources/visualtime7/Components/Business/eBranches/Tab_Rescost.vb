Option Strict Off
Option Explicit On
Public Class Tab_rescost
    '%-------------------------------------------------------%'
    '% $Workfile:: Tab_rescost                              $%'
    '% $Author:: HMendez                                    $%'
    '% $Date:: 14/08/03 16:38p                              $%'
    '% $Revision:: 10                                       $%'
    '%-------------------------------------------------------%'

    '+ Definición de la tabla TAB_RESCOST tomada el 14/08/2013 11:11
    '+ Column_Name                          Type        Length   Prec   Scale  Nullable
    ' ------------------------------ --------------- - -------- ------- ----- --------------
    Public nCodcost As Integer      ' NUMBER                5        5      0       No
    Public dEffecdate As Date    ' Char                  30       5      0       No
    Public sDescript As String      ' Char                  30       5      0       No
    Public nRate As Double         ' NUMBER                5                       No
    Public nCurrency As Integer     ' NUMBER                5                       Yes
    Public nAmount As Double       ' NUMBER                18                      Yes
    Public nMinimum As Double      ' NUMBER                18                      No
    Public nMaximum As Double      ' NUMBER                18                      No
    Public nUsercode As Integer ' NUMBER        22     5      0 No

    '%Add: Crea un registro en la tabla
    Public Function Add() As Boolean
        Add = InsUpdTab_rescost(1)

    End Function

    '%Update: Actualiza los datos de la tabla
    Public Function Update() As Boolean
        Update = InsUpdTab_rescost(2)
    End Function

    '%Delete: Borra los datos de la tabla
    Public Function Delete() As Boolean
        Delete = InsUpdTab_rescost(3)
    End Function

    '%InsValTab_rescost: Lee los datos de la tabla Tab_rescost
    Public Function InsValTab_rescost(ByVal nCodcost As Integer, ByVal dEffecdate As Date) As Boolean
        Dim lrecreaTab_rescost_v As eRemoteDB.Execute
        Dim nExist As Integer

        On Error GoTo reaTab_rescost_v_Err

        lrecreaTab_rescost_v = New eRemoteDB.Execute

        With lrecreaTab_rescost_v
            .StoredProcedure = "reaTab_rescost_v"
            .Parameters.Add("nCodcost", nCodcost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", nExist, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            If .Parameters("nExist").Value = 1 Then
                InsValTab_rescost = True
            End If
        End With

reaTab_rescost_v_Err:
        If Err.Number Then
            InsValTab_rescost = False
        End If
        'UPGRADE_NOTE: Object lrecreaTab_rescost_v may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaTab_rescost_v = Nothing
        On Error GoTo 0
    End Function

    '%insValMCA300_k: Esta función se encarga de validar los datos del encabezado

    Public Function insValMCA300_k(ByVal sCodispl As String, ByVal nMainAction As Integer, ByVal dEffecdate As Date) As String
        '- Se definen los objetos para el manejo de las clases
        Dim lobjErrors As eFunctions.Errors
        Dim lblnError As Boolean
        Dim ldtmDate As Date

        On Error GoTo insValMCA300_k_Err

        lobjErrors = New eFunctions.Errors

        lblnError = False

        '+ Validación de fecha
        With lobjErrors
            If dEffecdate = dtmNull Then
                lblnError = True
                Call .ErrorMessage(sCodispl, 4003)
            End If

            '+ Validacion de fecha de actualización
            If Not lblnError Then
                If nMainAction <> eFunctions.Menues.TypeActions.clngActionQuery Then
                    ldtmDate = Find_Date_Greater()
                    If ldtmDate <> dtmNull Then
                        If dEffecdate < ldtmDate Then
                            Call .ErrorMessage(sCodispl, 55611, , eFunctions.Errors.TextAlign.RigthAling, " (" & ldtmDate & ")")
                        End If
                    End If
                End If
            End If

            insValMCA300_k = .Confirm
        End With

insValMCA300_k_Err:
        If Err.Number Then
            insValMCA300_k = "insValMCA300_k: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '%Find_Date_Greater Valida la fecha de efecto de la transacción
    Public Function Find_Date_Greater() As Date
        Dim lrecTab_rescost As eRemoteDB.Execute
        Dim ldtmDate As Date

        On Error GoTo Find_Date_Greater_Err

        Find_Date_Greater = dtmNull

        lrecTab_rescost = New eRemoteDB.Execute

        With lrecTab_rescost
            .StoredProcedure = "ReaTab_rescost_date"
            .Parameters.Add("dEffecdate", ldtmDate, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            Find_Date_Greater = .Parameters("dEffecdate").Value
        End With

Find_Date_Greater_Err:
        If Err.Number Then
            Find_Date_Greater = dtmNull
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecTab_rescost may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecTab_rescost = Nothing
    End Function

    '%insValMCA300: Esta función se encarga de validar los datos del Form
    '%Ramos/Productos permitidos para el descuento por volúmen
    Public Function InsValMCA300(ByVal sCodispl As String, ByVal sAction As String, ByVal nCodcost As Integer, ByVal dEffecdate As Date, ByVal sDescript As String, ByVal nRate As Double, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nMinimum As Double, ByVal nMaximum As Double) As String
        '- Se define el objeto para el manejo de las clases
        Dim lobjErrors As eFunctions.Errors
        Dim mobjProduct As eBranches.Tab_rescost

        Dim lblnError As Boolean

        On Error GoTo insValMCA300_Err

        lobjErrors = New eFunctions.Errors


        With lobjErrors
            '+ Validación ingreso Codigo
            If nCodcost = eRemoteDB.Constants.intNull Then
                lblnError = True
                Call .ErrorMessage(sCodispl, 700001, , eFunctions.Errors.TextAlign.RigthAling, "Codigo")
            End If
            '+ Validación ingreso Descripción
            If sDescript = eRemoteDB.Constants.strNull Then
                lblnError = True
                Call .ErrorMessage(sCodispl, 700001, , eFunctions.Errors.TextAlign.RigthAling, "Descripción")
            End If
            '+ Procentaje excede 100%
            If nRate > 100 Then
                lblnError = True
                Call .ErrorMessage(sCodispl, 11239)
            End If
            '+ Debe ingresar monto o porcentaje
            If nRate = eRemoteDB.Constants.intNull And nAmount = eRemoteDB.Constants.intNull Then
                lblnError = True
                Call .ErrorMessage(sCodispl, 700001, , eFunctions.Errors.TextAlign.RigthAling, "Monto o Porcentaje")
            End If
            '+ Debe ingresar moneda si ingresa monto
            If nAmount <> eRemoteDB.Constants.intNull Then
                If nCurrency = 0 Or nCurrency = eRemoteDB.Constants.intNull Then
                    lblnError = True
                    Call .ErrorMessage(sCodispl, 10107)
                End If
                If nMaximum <> eRemoteDB.Constants.intNull Then
                    '+ MaXimo Excede al MONTO
                    If nMaximum > nAmount Then
                        lblnError = True
                        Call .ErrorMessage(sCodispl, 3194)
                    Else
                        If nMinimum <> eRemoteDB.Constants.intNull Then
                            '+ Minimo Excede al Maximo
                            If nMinimum > nMaximum Then
                                lblnError = True
                                Call .ErrorMessage(sCodispl, 10167)
                            End If
                        End If
                    End If
                Else
                    If nMinimum <> eRemoteDB.Constants.intNull Then
                        '+ Minimo Excede al Monto
                        If nMinimum > nAmount Then
                            lblnError = True
                            Call .ErrorMessage(sCodispl, 10167)
                        End If
                    End If
                End If
            End If


            '+ Validación de duplicidad CodCost en la Fecha
            If sAction = "Add" Then
                If Not lblnError Then
                    If InsValTab_rescost(nCodcost, dEffecdate) Then
                        Call .ErrorMessage(sCodispl, 20029)
                    End If
                End If
            End If

            '+ Validación del estado del registro

            InsValMCA300 = .Confirm
        End With

insValMCA300_Err:
        If Err.Number Then
            InsValMCA300 = "insValMCA300: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lobjErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lobjErrors = Nothing
    End Function

    '%InsPostMCA300Upd: Esta función realiza los cambios de BD según especificaciones funcionales
    '%                 de la transacción (MCA300)
    Public Function InsPostMCA300Upd(ByVal sAction As String, ByVal nCodcost As Integer, ByVal dEffecdate As Date, ByVal sDescript As String, ByVal nRate As Double, ByVal nCurrency As Integer, ByVal nAmount As Double, ByVal nMinimum As Double, ByVal nMaximum As Double, ByVal nUsercode As Integer) As Boolean
        Dim lintAction As Integer

        On Error GoTo InsPostMCA300Upd_Err
        With Me
            .nCodcost = nCodcost
            .dEffecdate = dEffecdate
            .sDescript = sDescript
            .nRate = nRate
            .nCurrency = nCurrency
            .nAmount = nAmount
            .nMinimum = nMinimum
            .nMaximum = nMaximum
            .nUsercode = nUsercode

            If sAction = "Del" Then
                lintAction = 3
            Else
                If sAction = "Update" Then
                    lintAction = 2
                Else
                    If sAction = "Add" Then
                        lintAction = 1
                    End If
                End If
            End If

            Select Case lintAction
                Case 1

                    '+ Se crea el registro
                    InsPostMCA300Upd = .Add

                    '+ Se modifica el registro
                Case 2
                    InsPostMCA300Upd = .Update

                    '+ Se elimina el registro
                Case 3
                    InsPostMCA300Upd = .Delete

            End Select
        End With

InsPostMCA300Upd_Err:
        If Err.Number Then
            InsPostMCA300Upd = False
        End If
        On Error GoTo 0
    End Function

    '%InsUpdTab_rescost: Realiza la actualización de la tabla
    Private Function InsUpdTab_rescost(ByVal nAction As Integer) As Boolean
        Dim lrecInsUpdTab_rescost As eRemoteDB.Execute

        On Error GoTo InsUpdTab_rescost_Err

        lrecInsUpdTab_rescost = New eRemoteDB.Execute

        With lrecInsUpdTab_rescost
            .StoredProcedure = "InsUpdTab_rescost"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCodcost", nCodcost, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sDescript", sDescript, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 2, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMinimum", nMinimum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nMaximum", nMaximum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 6, 18, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsUpdTab_rescost = .Run(False)
        End With

InsUpdTab_rescost_Err:
        If Err.Number Then
            InsUpdTab_rescost = False
        End If
        'UPGRADE_NOTE: Object lrecInsUpdTab_rescost may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecInsUpdTab_rescost = Nothing
        On Error GoTo 0
    End Function

    '* Class_Initialize: se controla la apertura de la clase
    '---------------------------------------------------------
    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()
        '---------------------------------------------------------
        '  nBranch = eRemoteDB.Constants.intNull
        '  nProduct = eRemoteDB.Constants.intNull
        '  dEffecdate = dtmNull
        '  sStatregt = String.Empty
        ' dNulldate = dtmNull
        ' nUsercode = eRemoteDB.Constants.intNull
    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub


End Class






