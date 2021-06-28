Option Strict Off
Option Explicit On
Public Class Plan_intwar_day
	'%-------------------------------------------------------%'
	'% $Workfile:: Plan_intwar_day.cls                           $%'
	'% $Author:: Mpalleres                                  $%'
	'% $Date:: 30-09-09 12:42                               $%'
	'% $Revision:: 5                                        $%'
	'%-------------------------------------------------------%'
	
	'**- properties according the table in the system 09/04/2001
	'**- Fund value " nominal values of the units of a fund"
	'- Propiedades según la tabla en el sistema 09/04/2001
	'- Plan_intwar_day "Valor nominal de las unidades de un fondo"
	
	'Column_name                  Type              Computed        Length      Prec  Scale Nullable       TrimTrailingBlanks     FixedLenNullInSource
	'--------------------------- -------------------------------------------------------------------------------------------------------------------------
    Public nTypeinvest As Integer 'smallint           no              2           5     0     no                 (n/a)                   (n/a)
    Public nRate As Double 'decimal            no              9           12    6     yes                (n/a)                   (n/a)
    Public dEffecdate As Date 'datetime           no              8                       no                 (n/a)                   (n/a)
    Public dNulldate As Date 'datetime           no              8                       yes                (n/a)                   (n/a)
    Public sFoundDescript As String
    Public nUsercode As Integer 'smallint           no              2           5     0     no                 (n/a)                   (n/a)


    '**% insPostMVI012: Updates the data of the form.
    '% insPostMVI012: Actualiza los datos de la forma.
    Public Function insPostMVI012(ByVal sAction As String, _
                                  ByVal nTypeinvest As Integer, _
                                  ByVal nRate As Double, _
                                  ByVal dEffecdate As Date, _
                                  ByVal nUsercode As Integer) As Boolean

        On Error GoTo insPostMVI012_err

        Dim lclsPlan_intwar_day As Plan_intwar_day
        lclsPlan_intwar_day = New Plan_intwar_day

        With lclsPlan_intwar_day
            .nTypeinvest = nTypeinvest
            .dEffecdate = dEffecdate
            .nRate = nRate
            .nUsercode = nUsercode

            Select Case sAction

                '**+ If the selected option is Record.
                '+ Si la opción seleccionada es Registrar

                Case "Add"
                    .dNulldate = eRemoteDB.Constants.dtmNull
                    insPostMVI012 = .Add

                    '**+ If the selected option is Modify
                    '+ Si la opción seleccionada es Modificar

                Case "Update"
                    insPostMVI012 = .Update
            End Select
        End With

insPostMVI012_err:
        If Err.Number Then insPostMVI012 = False

        'UPGRADE_NOTE: Object lclsPlan_intwar_day may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPlan_intwar_day = Nothing
    End Function

    '**% insValMVI012: Allows to make the proper validates of the transaction
    '% insValMVI012: Realiza las validaciones propias de la transacción.
    Public Function insValMVI012(ByVal sCodispl As String, ByVal nTypeinvest As Integer, ByVal nRate As Double, ByVal sSchema_code As String, ByVal dFundDate As Date, ByVal dEffecdate As Date) As String
        On Error GoTo insValMVI012_Err

        Dim lclsErrors As eFunctions.Errors
        Dim lclsScheCur As eSecurity.Secur_sche
        Dim lclsPlan_intwar_days As Plan_intwar_days
        Dim dMaxDate As Date
        lclsErrors = New eFunctions.Errors
        lclsScheCur = New eSecurity.Secur_sche
        lclsPlan_intwar_days = New Plan_intwar_days

        '+ Validación del campo "Fondo".
        If nTypeinvest <= 0 Then
            Call lclsErrors.ErrorMessage(sCodispl, 60422)
        End If


        '+ Validación del "Valor de la unidad de fondos".
        If nRate = 0 Or nRate = eRemoteDB.Constants.intNull Then
            Call lclsErrors.ErrorMessage(sCodispl, 60389)
        End If
        dMaxDate = FindMaxDate(dEffecdate,nTypeinvest)
        '+ Se verifica que la fecha sea posterior a la de la última transacción
        If dMaxDate > dEffecdate Then
            Call lclsErrors.ErrorMessage(sCodispl, 90000045 , , ," ("& dMaxDate &")")
        Else
            If Not FindDateVal(dEffecdate, nTypeinvest) Then
                lclsErrors.sTypeMessage = eFunctions.Errors.ErrorsType.ErrorTyp
                Call lclsErrors.ErrorMessage(sCodispl, 80500)
            Else
                '+ No debe permitir ingresar valor cuota a una fecha futura, si el día hábil precedente
                '+ no tiene valor cuota ingresado
                If Not ValHollidayExist(dEffecdate, nTypeinvest) Then
                    Call lclsErrors.ErrorMessage(sCodispl, 80502)
                End If
            End If
        End If  
        insValMVI012 = lclsErrors.Confirm

insValMVI012_Err:
        If Err.Number Then insValMVI012 = insValMVI012 & Err.Description

        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsScheCur may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsScheCur = Nothing
        'UPGRADE_NOTE: Object lclsPlan_intwar_days may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsPlan_intwar_days = Nothing

        On Error GoTo 0
    End Function

    '% insValMVI012: Realiza las validaciones correspondientes, según lo indica el funcional de
    '% la transacción.
    Public Function insValMVI012_k(ByVal sCodispl As String, ByVal dEffecdate As Date) As String
        On Error GoTo insValMVI012_k_err

        Dim lclsError As eFunctions.Errors

        lclsError = New eFunctions.Errors

        '+ Se verifica que la fecha sea válida

        If dEffecdate = eRemoteDB.Constants.dtmNull Then
            Call lclsError.ErrorMessage(sCodispl, 4003)
        End If

        insValMVI012_k = lclsError.Confirm

insValMVI012_k_err:
        If Err.Number Then insValMVI012_k = "insValMVI012_k: " & Err.Description

        'UPGRADE_NOTE: Object lclsError may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsError = Nothing

        On Error GoTo 0
    End Function

    '**% Find:Allows search the nominal value of the fund units
    '% Find: Busca el valor nominal de las unidades de un fondo
    Public Function Find() As Boolean
        Dim lrecreaPlan_intwar_day As eRemoteDB.Execute

        lrecreaPlan_intwar_day = New eRemoteDB.Execute

        On Error GoTo Find_Err

        '**+ Parameters definition to stored procedure ' insudb.reaPlan_intwar_day'
        '**+ Data read on 04/09/2001 04:33:59 PM
        '+ Definición de parámetros para stored procedure 'insudb.reaPlan_intwar_day'
        '+ Información leída el 09/04/2001 04:33:59 PM

        With lrecreaPlan_intwar_day
            .StoredProcedure = "reaPlan_intwar_day"

            .Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecDate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                nRate = .FieldToClass("nRate")
                dEffecdate = .FieldToClass("dEffecDate")

                Find = True
                .RCloseRec()
            End If
        End With

Find_Err:
        If Err.Number Then Find = False

        'UPGRADE_NOTE: Object lrecreaPlan_intwar_day may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaPlan_intwar_day = Nothing
    End Function

    '**% Add: Allows to create a record in the nominal value table of the fund units.
    '% Add: Permite crear un registro en la tabla de Valor nominal de las unidades de un fondo.
    Public Function Add() As Boolean
        Dim lreccrePlan_intwar_day As eRemoteDB.Execute

        lreccrePlan_intwar_day = New eRemoteDB.Execute

        On Error GoTo Add_err

        Add = True

        '**+ Parameters definition to stored procedure 'insudb.crePlan_intwar_day'
        '**+ Data read on 04/09/2001 15:08:39
        '+ Definición de parámetros para stored procedure 'insudb.crePlan_intwar_day'
        '+ Información leída el 09/04/2001 15:08:39

        With lreccrePlan_intwar_day
            .StoredProcedure = "crePlan_intwar_day"

            .Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dNulldate", dNulldate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Add = .Run(False)
        End With

Add_err:
        If Err.Number Then Add = False

        'UPGRADE_NOTE: Object lreccrePlan_intwar_day may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lreccrePlan_intwar_day = Nothing
    End Function

    '**% Update: Allows to update a record in the nominal value table of the fund units.
    '% Update: Permite actualizar un registro en la tabla de Valor nominal de las unidades de un fondo.
    Public Function Update() As Boolean
        Dim lrecupdPlan_intwar_day As eRemoteDB.Execute

        lrecupdPlan_intwar_day = New eRemoteDB.Execute

        On Error GoTo Update_Err

        Update = True

        '**+ Parameters definition to stored procedure ' insudb.upPlan_intwar_day'
        '**+ Data read on 04/06/2001 15:11:25
        '+ Definición de parámetros para stored procedure 'insudb.updPlan_intwar_day'
        '+ Información leída el 06/04/2001 15:11:25

        With lrecupdPlan_intwar_day
            .StoredProcedure = "updPlan_intwar_day"

            .Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nRate", nRate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 6, 12, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

Update_Err:
        If Err.Number Then Update = False

        'UPGRADE_NOTE: Object lrecupdPlan_intwar_day may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecupdPlan_intwar_day = Nothing
    End Function

    '**% Delete: Allows to delete a record of the nominal value table of the fund units.
    '% Delete : Permite eliminar un registro de la tabla de Valor nominal de las unidades de un fondo.
    Public Function Delete() As Boolean
        Dim lrecdelPlan_intwar_day As eRemoteDB.Execute

        lrecdelPlan_intwar_day = New eRemoteDB.Execute

        On Error GoTo Delete_err

        Delete = True

        '**+ Parameters definition to stored procedure 'insudb.delPlan_intwar_day'
        '**+ Data read on 04/09/2001  15:13:24
        '+ Definición de parámetros para stored procedure 'insudb.delPlan_intwar_day'
        '+ Información leída el 09/04/2001 15:13:24

        With lrecdelPlan_intwar_day
            .StoredProcedure = "delPlan_intwar_day"

            .Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then Delete = False

        'UPGRADE_NOTE: Object lrecdelPlan_intwar_day may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelPlan_intwar_day = Nothing
    End Function


    'ValHollidayExist: Función que valida la existencia de valor cuota para un día hábil precedente a la fecha ingresada
    Public Function ValHollidayExist(ByVal dEffecdate As Date, ByVal nTypeinvest As Integer) As Boolean
        Dim lclsFund As eRemoteDB.Execute
        Dim lblnExist As Boolean

        On Error GoTo ValHollidayExist_Err
        lclsFund = New eRemoteDB.Execute

        With lclsFund
            .StoredProcedure = "Valmodholliday"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                If .FieldToClass("nExist") = 1 Then
                    ValHollidayExist = True
                End If
            End If
        End With
        'UPGRADE_NOTE: Object lclsFund may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsFund = Nothing

ValHollidayExist_Err:
        If Err.Number Then
            ValHollidayExist = False
        End If
        On Error GoTo 0
    End Function


    '% FindDateVal: Selecciona la última fecha en lacual se permite la modificaion
    ' de valores par aun fondo
    Public Function FindDateVal(ByVal dEffecdate As Date, ByVal nTypeinvest As Integer) As Boolean
        Dim lrecFindDateVal As eRemoteDB.Execute
        lrecFindDateVal = New eRemoteDB.Execute

        With lrecFindDateVal
            .StoredProcedure = "Finddateval_mod"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nExist", eRemoteDB.Constants.intNull, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbSmallInt, 22, 0, 1, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                FindDateVal = .Parameters("nExist").Value = 1
            End If
        End With


        'UPGRADE_NOTE: Object lrecFindDateVal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecFindDateVal = Nothing
    End Function

    Public Function FindMaxDate(ByVal dEffecdate As Date, ByVal nTypeinvest As Integer) As Date
        Dim lrecFindDateVal As eRemoteDB.Execute
        lrecFindDateVal = New eRemoteDB.Execute

        With lrecFindDateVal
            .StoredProcedure = "FindMaxDeffecdateTypeInvest"
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInputOutput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeinvest", nTypeinvest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                FindMaxDate = .Parameters("dEffecdate").Value
            End If
        End With
        'UPGRADE_NOTE: Object lrecFindDateVal may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecFindDateVal = Nothing
    End Function
End Class






