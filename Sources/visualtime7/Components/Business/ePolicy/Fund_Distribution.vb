Option Explicit On
Public Class Fund_distribution
    Public nBranch As Double 'NUMBER(10)   NOT NULL,
    Public nProduct As Double 'NUMBER(10)   NOT NULL,
    Public nTypeprofile As Double 'number(10)   not null,
    Public dEffecdate As Date 'date         not null,
    Public dNulldate As Date
    Public nFunds As Double ' number(10),
    Public nOrigin As Double 'number(10),
    Public nPercent As Double ' number(5,2),
    Public dCompdate As Date         'not null,
    Public nUsercode As Double
    '**% Find_date: read the information of the an investment funds
    '% Find_date: Permite seleccionar la información de un fondo
    Public Function Find_date(ByVal nBranch As Integer, ByVal nProduct As Double, ByVal nTypeProfile As Double) As Boolean
        Dim lrecreaFund_distribution_1 As eRemoteDB.Execute

        lrecreaFund_distribution_1 = New eRemoteDB.Execute

        On Error GoTo Find_date_Err

        Find_date = True

        With lrecreaFund_distribution_1
            .StoredProcedure = "reaFund_Distribution_Effec"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeprofile", nTypeProfile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run Then
                dEffecdate = .FieldToClass("dEffecdate")
                .RCloseRec()
            Else
                Find_date = False
            End If
        End With

        'UPGRADE_NOTE: Object lrecreaFund_distribution_1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaFund_distribution_1 = Nothing

Find_date_Err:
        If Err.Number Then
            Find_date = False
        End If
    End Function
    '**% insValMVI70: This function performed the validation of the page.
    '% insValMVI70: Realiza las validaciones propias de la transacción.
    Public Function insValMVI70(ByVal sCodispl As String, ByVal nActions As Integer, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nTypeProfile As Double, ByVal dEffecdate As Date) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsvalfield As eFunctions.valField
        Dim lrecinsinsValMVI70 As Object

        '**- The variable to show the validation related to status of the record only when
        '**- the user entered the rest of the field is defined
        '- Variable que permitira mostrar la validación asociada al estado del registro solo
        '- cuando se indiquen el resto de los campos

        Dim lblnError As Object
        Dim lstrErrors As String

        lclsErrors = New eFunctions.Errors
        lclsvalfield = New eFunctions.valField
        lclsvalfield.objErr = lclsErrors
        On Error GoTo insValMVI70_Err

        lrecinsinsValMVI70 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
        With lrecinsinsValMVI70
            .StoredProcedure = "INSMVI70PKG.INSVALMVI70_K"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nActions", nActions, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeprofile", nTypeProfile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrErrors = .Parameters("Arrayerrors").Value
        End With
        lclsErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)


        insValMVI70 = lclsErrors.Confirm
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsvalfield = Nothing

insValMVI70_Err:
        If Err.Number Then
            insValMVI70 = insValMVI70 & Err.Description
        End If

        On Error GoTo 0
    End Function
   '**% insValMVI70_upd: This function performed the validation of the page.
    '% insValMVI70_upd: Realiza las validaciones propias de la transacción.
    Public Function insValMVI70_upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nTypeProfile As Double, ByVal dEffecdate As Date, ByVal nFunds As Double, ByVal nOrigin As Double, ByVal nPercent As Double) As String
        Dim lclsErrors As eFunctions.Errors
        Dim lclsvalfield As eFunctions.valField
        Dim lrecinsinsValMVI70_upd As Object

        '**- The variable to show the validation related to status of the record only when
        '**- the user entered the rest of the field is defined
        '- Variable que permitira mostrar la validación asociada al estado del registro solo
        '- cuando se indiquen el resto de los campos

        Dim lblnError As Object
        Dim lstrErrors As String

        lclsErrors = New eFunctions.Errors
        lclsvalfield = New eFunctions.valField
        lclsvalfield.objErr = lclsErrors
        On Error GoTo insValMVI70_upd_Err

        lrecinsinsValMVI70_upd = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
        With lrecinsinsValMVI70_upd
            .StoredProcedure = "INSMVI70PKG.insValMVI70_upd"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SACTION", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeprofile", nTypeProfile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Run(False)
            lstrErrors = .Parameters("Arrayerrors").Value
        End With
        lclsErrors.ErrorMessage(sCodispl, , , , , , lstrErrors)


        insValMVI70_upd = lclsErrors.Confirm
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsvalfield may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsvalfield = Nothing

insValMVI70_upd_Err:
        If Err.Number Then
            insValMVI70_upd = insValMVI70_upd & Err.Description
        End If

        On Error GoTo 0
    End Function

    '%InsPostmvi70_UpdUpd: Se realiza la actualización de los datos en la ventana mv70_upd
    Public Function InsPostmvi70_Upd(ByVal sCodispl As String, ByVal sAction As String, ByVal nBranch As Double, ByVal nProduct As Double, ByVal nTypeProfile As Double, ByVal dEffecdate As Date, ByVal nFunds As Double, ByVal nOrigin As Double, ByVal nPercent As Double, ByVal nUsercode As Integer) As Boolean
        Dim lclsValues As eFunctions.Values

        On Error GoTo InsPostmvi70_Upd_Err

        Dim lrecInsPostmvi70_Upd As eRemoteDB.Execute

        lrecInsPostmvi70_Upd = New eRemoteDB.Execute
        '+ Definición de store procedure InsPostmvi70_Upd al 08-29-2002 12:30:42
        With lrecInsPostmvi70_Upd
            .StoredProcedure = "INSMVI70PKG.INSPOSTMVI70"

            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypeprofile", nTypeProfile, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dEffecdate", dEffecdate, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Parameters.Add("nFunds", nFunds, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nOrigin", nOrigin, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nPercent", nPercent, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 2, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("SACTION", sAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 8, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            InsPostmvi70_Upd = .Run(False)

        End With



InsPostmvi70_Upd_Err:
        If Err.Number Then
            InsPostmvi70_Upd = False
        End If
        'UPGRADE_NOTE: Object lclsValues may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsValues = Nothing
        On Error GoTo 0
    End Function
End Class
