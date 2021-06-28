Public Class Folios_Agent
    Public nBranch As Integer
    Public nProduct As Integer
    Public nIntermed As Long
    Public dAssign_date As Date
    Public nStart As Long
    Public nEnd As Long
    Public nStartPolNumber As Long
    Public nEndPolNumber As Long

    Public sPolitype As String
    Public sProcessInd As String
    Public nUsercode As Integer

    Public sIntermedia As String
    Public sDesBranch As String
    Public sDesProd As String
    Public nSold As Integer

    Property sCause As String

    '% Find: 
    Public Function Find(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIntermed As Long, ByVal dAssign_date As Date, ByVal sPolitype As String, ByVal nStart As Long) As Boolean
        '- Se declara la variable que determina el resultado de la funcion (True/False)
        Static lblnRead As Boolean

        '- Se define la variable lrecreaFolios_agent
        Dim lrecreaFolios_agent As eRemoteDB.Execute

        lrecreaFolios_agent = New eRemoteDB.Execute

        With lrecreaFolios_agent
            .StoredProcedure = "reaFolios_agent"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAssign_date", dAssign_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                nBranch = .FieldToClass("nBranch")
                nProduct = .FieldToClass("nProduct")
                nIntermed = .FieldToClass("nIntermed")
                dAssign_date = .FieldToClass("dAssign_date")
                sPolitype = .FieldToClass("sPolitype")
                nStart = .FieldToClass("nStart")
                nEnd = .FieldToClass("nEnd")
                sProcessInd = .FieldToClass("sProcessInd")
                nStartPolNumber = .FieldToClass("nStartPolNumber")
                nEndPolNumber = .FieldToClass("nEndPolNumber")
                .RCloseRec()
                lblnRead = True
            Else
                lblnRead = False
            End If
        End With

        Find = lblnRead
        lrecreaFolios_agent = Nothing
    End Function

    '% ValExistFolios_agent: Valida que el rango incluida no exista en otro rango (Folios_agent)
    Public Function ValExistFolios_agent(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIntermed As Long, ByVal dAssign_date As Date, ByVal sPolitype As String, ByVal nStart As Long, ByVal nEnd As Long, ByVal saction As String, ByVal nStartPolNum As Long, ByVal nEndPolNum As Long) As Integer

        Dim lrecreaFolios_agent_v1 As eRemoteDB.Execute
        lrecreaFolios_agent_v1 = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaFolios_agent_v1'
        '+ Información leída el 06/07/2001 09:51:44 a.m.
        With lrecreaFolios_agent_v1
            ' Esta pendiente
            .StoredProcedure = "valConflictingFoliosAgent"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAssign_date", dAssign_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd", nEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStartPolNum", nStartPolNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndPolNum", nEndPolNum, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sAction", saction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                ValExistFolios_agent = .FieldToClass("nErrornum")
            End If

            .RCloseRec()
        End With

        lrecreaFolios_agent_v1 = Nothing
    End Function

    '% ValExistFolios_comp
    Public Function ValExistFolios_comp(ByVal nStart As Long, ByVal nEnd As Long, dAssign_date As Date) As Integer

        Dim lrecreaFolios_comp As eRemoteDB.Execute
        lrecreaFolios_comp = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.reaFolios_agent_v1'
        '+ Información leída el 06/07/2001 09:51:44 a.m.
        With lrecreaFolios_comp
            ' Esta pendiente
            .StoredProcedure = "VALFOLIOSCOMP"
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd", nEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAssign_date", dAssign_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run Then
                ValExistFolios_comp = .FieldToClass("nErrornum")
            End If

            .RCloseRec()
        End With

        lrecreaFolios_comp = Nothing
    End Function





    '% Add: Agrega un registro a la tabla de Folios asignados a la compañía (Folios_agent)
    Public Function Add() As Boolean
        Dim lreccreFolios_agent As eRemoteDB.Execute
        lreccreFolios_agent = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'insudb.creFolios_agent'
        '+ Información leída el 06/07/2001 05:37:41 p.m.
        With lreccreFolios_agent
            .StoredProcedure = "creFolios_agent"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAssign_date", dAssign_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd", nEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcessInd", sProcessInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStartPolNumber", nStartPolNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndPolNumber", nEndPolNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Add = .Run(False)
        End With

    End Function

    '% Update : Actualiza un registro en la tabla de Asignación de folios por intermediario (Folios_agent)
    Public Function Update() As Boolean
        Dim lrecupdFolios_agent As eRemoteDB.Execute

        On Error GoTo Update_err

        lrecupdFolios_agent = New eRemoteDB.Execute

        With lrecupdFolios_agent
            .StoredProcedure = "updFolios_agent"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAssign_date", dAssign_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEnd", nEnd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sProcessInd", sProcessInd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 6, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStartPolNumber", nStartPolNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nEndPolNumber", nEndPolNumber, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Update = .Run(False)
        End With

Update_err:
        If Err.Number Then
            Update = False
        End If
        On Error GoTo 0
        lrecupdFolios_agent = Nothing
    End Function

    '% Delete: Elimina un registro de la tabla de Asignación de folios por intermediario (Folios_agent)
    Public Function Delete() As Boolean
        Dim lrecdelFolios_agent As eRemoteDB.Execute

        On Error GoTo Delete_err

        lrecdelFolios_agent = New eRemoteDB.Execute

        With lrecdelFolios_agent
            .StoredProcedure = "delFolios_agent"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAssign_date", dAssign_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sPolitype", sPolitype, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStart", nStart, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            Delete = .Run(False)
        End With

Delete_err:
        If Err.Number Then
            Delete = False
        End If
        On Error GoTo 0
        lrecdelFolios_agent = Nothing
    End Function

    '% insValCA985: Valida los datos introducidos en la página
    '---------------------------------------------------------
    Public Function insValCA985(ByVal sCodispl As String, ByVal nZone As Integer, ByVal sWindowType As String, ByVal sAction As String, _
                                ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIntermed As Long, ByVal dAssign_date As Date, _
                                ByVal nStart As Long, ByVal nEnd As Long, ByVal sPolitype As String, ByVal nStartPolNumber As Long, ByVal nEndPolNumber As Long) As String
        '---------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValCA985_Err

        lclsErrors = New eFunctions.Errors

        ' Validaciones del encabezado
        If nZone = 1 Then
            ' Incluya el código del ramo.
            If nBranch <= 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 11135)
            End If

            'Incluya el código del producto.
            If nProduct <= 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 11009)
            End If

            'Incluya el código del intermediario.
            If nIntermed <= 0 Then
                Call lclsErrors.ErrorMessage(sCodispl, 3272)
            End If

            'Incluya la fecha.
            If dAssign_date = eRemoteDB.Constants.dtmNull Then
                Call lclsErrors.ErrorMessage(sCodispl, 9068)
            Else
                If dAssign_date > Today.Date Then
                    Call lclsErrors.ErrorMessage(sCodispl, 700005, , eFunctions.Errors.TextAlign.LeftAling, "Fecha de asignación")
                End If
            End If
        Else
            ' Validaciones de la parte masiva
            If sWindowType = "PopUp" Then

                'Incluya el rango inicial
                If nStart <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10247)
                End If

                'Incluya el rango final
                If nEnd <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 10248)
                End If

                ' Rango final inferior al rango inicial  
                If nStart > 0 And nEnd > 0 Then
                    If nEnd < nStart Then
                        Call lclsErrors.ErrorMessage(sCodispl, 10184)
                    End If
                End If

                'Incluya el rango inicial de póliza
                If nStartPolNumber <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7810247)
                End If

                'Incluya el rango final de póliza
                If nEndPolNumber <= 0 Then
                    Call lclsErrors.ErrorMessage(sCodispl, 7810248)
                End If

                ' Rango final inferior al rango inicial  
                If nStart > 0 And nEnd > 0 And nStartPolNumber > 0 And nEndPolNumber > 0 Then
                    If nEndPolNumber - nStartPolNumber <> nEnd - nStart Then
                        Call lclsErrors.ErrorMessage(sCodispl, 7810184)
                    End If
                End If

                ' Incluya el tipo de póliza                                                       
                If String.IsNullOrEmpty(sPolitype) Or sPolitype = "0" Then
                    Call lclsErrors.ErrorMessage(sCodispl, 5565)
                End If

                'ESTO ESTA PENDIENTE!!!!!
                ''El rango debe estar incluído en un rango valido de la tabla Folios_comp
                'If Not ValExistFolios_comp(nYear, nStart, nEnd) Then
                '    Call lclsErrors.ErrorMessage(sCodispl, 11138)
                'End If
                If nStart > 0 And nEnd > 0 Then
                    Dim fc As Integer = ValExistFolios_comp(nStart, nEnd, dAssign_date)
                    If fc > 0 Then
                        Call lclsErrors.ErrorMessage(sCodispl, fc)
                    Else
                        ' El rango no debe estar incluído en otro registro
                        Dim rv As Integer = ValExistFolios_agent(nBranch, nProduct, nIntermed, dAssign_date, sPolitype, nStart, nEnd, sAction, nStartPolNumber, nEndPolNumber)
                        If rv > 0 Then
                            Call lclsErrors.ErrorMessage(sCodispl, rv)
                        End If
                    End If
                End If
            End If
        End If
        insValCA985 = lclsErrors.Confirm

insValCA985_Err:
        If Err.Number Then
            insValCA985 = insValCA985 & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function



    '% insPostCA985: Actualiza los datos introducidos en la zona de contenido para "frame" especifico
    Public Function insPostCA985Upd(ByVal sAction As String, ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIntermed As Long, ByVal dAssign_date As Date, _
                                   ByVal nStart As Long, ByVal nEnd As Long, ByVal sPolitype As String, ByVal sProcessInd As String, ByVal nUsercode As Integer, _
                                   ByVal nStartPolNumber As Long, ByVal nEndPolNumber As Long) As Boolean

        With Me
            .nBranch = nBranch
            .nProduct = nProduct
            .nIntermed = nIntermed
            .dAssign_date = dAssign_date
            .nStart = nStart
            .nEnd = nEnd
            .sPolitype = sPolitype
            .sProcessInd = sProcessInd
            .nUsercode = nUsercode
            .nStartPolNumber = nStartPolNumber
            .nEndPolNumber = nEndPolNumber


            Select Case sAction.Trim
                Case "Add"
                    insPostCA985Upd = Add()
                Case "Del"
                    insPostCA985Upd = Delete()
                Case "Update"
                    insPostCA985Upd = Update()
            End Select
        End With

    End Function


    '% insPostCA985: Actualiza los datos introducidos en la zona de contenido para "frame" especifico
    Public Function insPostCA985(ByVal nBranch As Integer, ByVal nProduct As Integer, ByVal nIntermed As Long, ByVal dAssign_date As Date, ByVal nUsercode As Integer, Optional ByVal bPolicyNumberEqualToFolio As Boolean = False) As Boolean

        Dim lrecinsPostCA985 As eRemoteDB.Execute

        lrecinsPostCA985 = New eRemoteDB.Execute

        With lrecinsPostCA985
            .StoredProcedure = "insPostCA985pkg.insPostCA985"
            .Parameters.Add("nBranch", nBranch, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProduct", nProduct, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermed", nIntermed, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbNumeric, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dAssign_date", dAssign_date, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nSessionId", New Random().Next(100000000, 900000000), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostCA985 = .Run(False)
        End With

    End Function

    '% insValSOC001_K: Valida los datos introducidos en la página
    '---------------------------------------------------------
    Public Function insValSOC001_K(ByVal sCodispl As String, ByVal nYear As Integer) As String
        '---------------------------------------------------------
        Dim lclsErrors As eFunctions.Errors

        On Error GoTo insValSOC001_K_Err

        lclsErrors = New eFunctions.Errors

        ' Validaciones del Año
        If nYear <= 0 Then
            ' Incluya el Año.
            Call lclsErrors.ErrorMessage(sCodispl, 60338)
        Else
            '+ Debe ser un año válido
            If nYear < 1900 Then
                Call lclsErrors.ErrorMessage(sCodispl, 1183)
            End If
        End If

        insValSOC001_K = lclsErrors.Confirm

insValSOC001_K_Err:
        If Err.Number Then
            insValSOC001_K = insValSOC001_K & Err.Description
        End If
        On Error GoTo 0
        lclsErrors = Nothing
    End Function

    '% insValCA985: Valida los datos introducidos en la página
    '---------------------------------------------------------
    Public Function insValSO002_K(ByVal sCodispl As String, ByVal nZone As Integer, ByVal sWindowType As String, ByVal sAction As String, _
                                  ByVal nIntermedSource As Long, ByVal nFolioI As Long, ByVal nFolioE As Long, ByVal nIntermedDest As Long, _
                                ByVal nUsercode As Integer) As String
        '---------------------------------------------------------
        Dim lstrErrorAll As String = String.Empty
        Dim lclsErrors As eFunctions.Errors
        Dim lrecinsvalCA830 As eRemoteDB.Execute

        lrecinsvalCA830 = New eRemoteDB.Execute

        '+ Se invoca el SP para validar los campos de la transacción

        With lrecinsvalCA830
            .StoredProcedure = "INSSO002PKG.INSVALSO002_K"
            .Parameters.Add("sCodispl", sCodispl, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermedSource", nIntermedSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFolioI", nFolioI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFolioE", nFolioE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermedDest", nIntermedDest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", " ", eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            If .Run(False) Then
                lstrErrorAll = .Parameters("Arrayerrors").Value
            End If
        End With

        lclsErrors = New eFunctions.Errors

        With lclsErrors
            If Len(lstrErrorAll) > 0 Then
                Call .ErrorMessage(sCodispl, , , , , , lstrErrorAll)
            End If
            insValSO002_K = .Confirm
        End With
    End Function

    '% Find:Devuelve información de todas los registros 
    '%      de la tabla Folios asignados a la compañía (Folios_agent)
    Public Function PostSO002(ByVal nIntermedSource As Long, ByVal nFolioI As Long, ByVal nFolioE As Long, ByVal nIntermedDest As Long, _
                             ByVal nUsercode As Integer) As Boolean

        Dim lrecreaFolios_agent_a As eRemoteDB.Execute

        lrecreaFolios_agent_a = New eRemoteDB.Execute
        With lrecreaFolios_agent_a
            .StoredProcedure = "INSSO002PKG.INSPOSTSO002"
            .Parameters.Add("nIntermedSource", nIntermedSource, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFolioI", nFolioI, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nFolioE", nFolioE, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nIntermedDest", nIntermedDest, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            PostSO002 = .Run(False)
        End With
    End Function

End Class



