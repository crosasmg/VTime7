Option Strict Off
Option Explicit On
Public Class Document_Pay

    Private Const numNull As Short = -32768
    Private Const dtmNull As Date = #12:00:00 AM#
    '%-------------------------------------------------------%'
    '% $Workfile:: Document_Pay.cls                         $%'
    '% $Author:: Jacob S. / Partner Consulting Ltda.        $%'
    '% $Date:: 20/05/08 18:08 p                             $%'
    '% $Revision:: 0                                        $%'
    '%-------------------------------------------------------%'

    '+ Propiedades según la tabla en el sistema el 20/05/2008
    '+ El campo llave corresponde a nTypesupport.


    '+
    '+ Estructura de tabla Gaston L.Pay_Documents al 20-05-2008 18:14:01
    '+         Property                Type         DBType   Size Scale  Prec  Null
    '+-----------------------------------------------------------------------------

    Public nTypesupport As Integer ' NUMBER     5    0     0     N
    Public sClient As String ' CHAR       14   0     0     N
    Public nDocument As Double ' NUMBER     10   0     0     N
    Public nProvider As Integer ' NUMBER     5    0     0     N
    Public nAmount As Double ' NUMBER     18   0     6     S
    Public dDocument As Date ' DATE            0     0     S
    Public nStatus As Integer ' NUMBER     5    0     0     N
    Public sOpertype As String ' CHAR       2    0     0     S
    Public dStatdate As Date ' DATE            0     0     N
    Public dNulldate As Date ' DATE            0     0     S
    Public nUsercode As Integer ' NUMBER     22   0     5     N
    Public dCompdate As Date ' DATE            0     0     N
    Public nClaim As Double ' NUMBER     10   0     0     S
    Public nCurrency As Integer ' NUMBER     5    0     0     N
    Public nServ_order As Integer ' NUMBER     10   0     0     N

    '+ Variables auxiliares

    Public sCliename As String ' CHAR       60   0     0     S
    Public nTypeprov As Integer ' NUMBER      0   0     0     N
    Public dInpdate As Date ' DATE            0     0     S
    Public dOutdate As Date ' DATE            0     0     S
    Public nOffice As Integer ' NUMBER     10   0     0     N
    Public nMax_serv_ord As Integer ' NUMBER     10   0     0     N
    Public nPer_disc As Integer ' NUMBER     10   0     0     N
    Public sConcesionary As String ' CHAR       2    0     0     S
    Public nAction As Integer ' NUMBER     5    0     0     N
    Public sDescript As String ' CHAR       30   0     0     S
    Public nIdconsec As Integer ' NUMBER     5    0     0     N
    Public nTyp_acco As Integer ' NUMBER     5    0     0     N
    Public dOperdate As Date ' DATE            0     0     S
    Public nId As Integer ' NUMBER     10   0     0     N
    Public sSel As String
    Public sKey As String
    Public nCredit As Integer ' NUMBER     18   0     6     S
    Public nDebit As Integer ' NUMBER     18   0     6     S
    Public nCheque As Double

    '% add
    Public Function Add() As Boolean

        Dim lrecDoc_Pay As eRemoteDB.Execute
        lrecDoc_Pay = New eRemoteDB.Execute
        On Error GoTo Add_err

        With lrecDoc_Pay
            .StoredProcedure = "INSNC001PKG.creDoc_Pay"
            .Parameters.Add("nTypesupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 5, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAmount", nAmount, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 18, 0, 6, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dDocument", IIf(dDocument = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dDocument), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Add = .Run(False)
        End With
        'UPGRADE_NOTE: Object lrecDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecDoc_Pay = Nothing

Add_err:
        If Err.Number Then
            Add = False
        End If
        On Error GoTo 0
    End Function
    '% FindSI008: Busca la información de un determinado Documento
    Public Function FindSI008(ByVal nDocument As Double, ByVal sClient As String) As Boolean

        Dim lrecreaDoc_Pay As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecreaDoc_Pay = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'rea_si008_doc'
        With lrecreaDoc_Pay
            .StoredProcedure = "insnc002pkg.rea_si008_doc"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then

                Me.nTypesupport = .FieldToClass("nTypesupport")
                Me.dDocument = .FieldToClass("dDocument")

                .RCloseRec()
                FindSI008 = True
            Else
                FindSI008 = False
            End If
        End With

Find_Err:
        If Err.Number Then
            FindSI008 = False
        End If

        'UPGRADE_NOTE: Object lrecreaDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDoc_Pay = Nothing

        On Error GoTo 0
    End Function

    '% FindProvNC003: Busca la información de del proveedor de acuerda a la orden de servicio ingresada
    Public Function FindProvNC003(ByVal nServ_order As Double) As Boolean
        Dim lrecreaDoc_Pay As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecreaDoc_Pay = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'rea_si008_doc'
        With lrecreaDoc_Pay
            .StoredProcedure = "insnc003pkg.insreanc003prov"
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            If .Run(True) Then

                Me.sCliename = .FieldToClass("sCliename")
                Me.sClient = .FieldToClass("sClient")
                Me.nProvider = .FieldToClass("nProvider")

                .RCloseRec()
                FindProvNC003 = True
            Else
                FindProvNC003 = False
            End If
        End With

Find_Err:
        If Err.Number Then
            FindProvNC003 = False
        End If

        'UPGRADE_NOTE: Object lrecreaDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDoc_Pay = Nothing

        On Error GoTo 0
    End Function
    '% Find: Busca la información de un determinado Documento
    Public Function Find(ByVal nTypesupport As Integer, ByVal sClient As String, ByVal nDocument As Double, ByVal nStatus As Integer, Optional ByVal dStatus1 As Date = #12:00:00 AM#, Optional ByVal dStatus2 As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean
        Dim lrecreaDoc_Pay As eRemoteDB.Execute

        On Error GoTo Find_Err

        lrecreaDoc_Pay = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'reaDoc_Pay'


        With lrecreaDoc_Pay
            .StoredProcedure = "INSNC002PKG.reaDoc_Pay"
            .Parameters.Add("nTypesupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStatdate1", IIf(dStatus1 = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dStatus1), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("dStatdate2", IIf(dStatus2 = eRemoteDB.Constants.dtmNull, System.DBNull.Value, dStatus2), eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDBTimeStamp, 0, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



            If .Run(True) Then

                Me.nTypesupport = .FieldToClass("nTypesupport")
                Me.sClient = .FieldToClass("sClient")
                Me.nDocument = .FieldToClass("nDocument")
                Me.nProvider = .FieldToClass("nProvider")
                Me.nAmount = .FieldToClass("nAmount")
                Me.dDocument = .FieldToClass("dDocument")
                Me.nStatus = .FieldToClass("nStatus")
                Me.sOpertype = .FieldToClass("sOpertype")
                Me.dStatdate = .FieldToClass("dStatdate")
                Me.dNulldate = .FieldToClass("dNulldate")
                Me.nUsercode = .FieldToClass("nUsercode")
                Me.dCompdate = .FieldToClass("dCompdate")
                Me.nClaim = .FieldToClass("nClaim")
                Me.nCurrency = .FieldToClass("nCurrency")
                Me.nServ_order = .FieldToClass("nServ_order")
                Me.sCliename = .FieldToClass("sCliename")

                .RCloseRec()
                Find = True
            Else
                Find = False
            End If
        End With

Find_Err:
        If Err.Number Then
            Find = False
        End If

        'UPGRADE_NOTE: Object lrecreaDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaDoc_Pay = Nothing

        On Error GoTo 0
    End Function

    '% Find_Provider: Busca la información de un determinado Proveedor
    Public Function Find_Provider(ByVal sClient As String) As Boolean
        Dim rdbParamNullable As Object
        Dim rdbVarChar As Object
        Dim lrecreaProvider As eRemoteDB.Execute
        On Error GoTo Find_P_Err

        lrecreaProvider = New eRemoteDB.Execute

        '+ Definición de parámetros para stored procedure 'reaProvider'


        With lrecreaProvider
            .StoredProcedure = "INSNC001PKG.REA_PROVIDER"
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            If .Run(True) Then

                Me.nProvider = .FieldToClass("nProvider")
                Me.nTypeprov = .FieldToClass("nTypeprov")
                Me.sClient = .FieldToClass("sClient")
                Me.dInpdate = .FieldToClass("dInpdate")
                Me.dOutdate = .FieldToClass("dOutdate")
                Me.nOffice = .FieldToClass("nOffice")
                Me.nMax_serv_ord = .FieldToClass("nMax_serv_ord")
                Me.nTypesupport = .FieldToClass("nTypesupport")
                Me.nPer_disc = .FieldToClass("nPer_disc")
                Me.sConcesionary = .FieldToClass("sConcesionary")

                .RCloseRec()
                Find_Provider = True
            Else
                Find_Provider = False
            End If
        End With

Find_P_Err:
        If Err.Number Then
            Find_Provider = False
        End If

        'UPGRADE_NOTE: Object lrecreaProvider may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecreaProvider = Nothing

        On Error GoTo 0
    End Function


    '% Remove: Elimina la información de un determinado documento de pago
    Public Function Remove(ByVal nDocument As Double) As Boolean

        Dim lrecdelDoc_Pay As eRemoteDB.Execute

        'Definición de parámetros para stored procedure 'INSNC001PKG.DELDOC_PAY'

        On Error GoTo Remove_Err

        lrecdelDoc_Pay = New eRemoteDB.Execute

        With lrecdelDoc_Pay
            .StoredProcedure = "INSNC001PKG.DELDOC_PAY"
            .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbDouble, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            Remove = .Run(False)
        End With

Remove_Err:
        If Err.Number Then
            Remove = False
        End If

        'UPGRADE_NOTE: Object lrecdelDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecdelDoc_Pay = Nothing

        On Error GoTo 0
    End Function


    '% insValNC001:Validacion de la Ventana NC001_K
    Public Function insValNC001_K(ByVal nTypesupport As Integer, ByVal sClient As String, ByVal nProvider As Integer, ByVal nDocument As Double, ByVal nAmount As Double) As String
        Dim eClaim As Object
        'Dim lclsErrors   As eFunctions.Errors
        Dim lclsErrors As Object
        Dim lclsDoc_Pay As eClaim.Document_Pay
        Dim blnError As Boolean

        On Error GoTo insValNC001_K_Err

        lclsDoc_Pay = New eClaim.Document_Pay
        'Set lclsErrors = New eFunctions.Errors
        lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")

        insValNC001_K = CStr(True)

        blnError = False

        '+Validacion del Tipo de documento.
        If nTypesupport <= 0 Then
            Call lclsErrors.ErrorMessage("NC001_K", 60505)
            blnError = True
        End If

        '+Validacion de Rut del Proveedor.
        If Trim(sClient) = String.Empty Then
            Call lclsErrors.ErrorMessage("NC001_K", 55769)
            blnError = True
        End If

        '+Validacion del codigo de Proveedor.
        If nProvider <= 0 Then
            Call lclsErrors.ErrorMessage("NC001_K", 10908)
            blnError = True
        End If

        '+Validacion del numero de documento.
        If nDocument <= 0 Then
            Call lclsErrors.ErrorMessage("NC001_K", 5045)
            blnError = True
        End If

        '+Validacion del monto de documento.
        If nAmount <= 0 Then
            Call lclsErrors.ErrorMessage("NC001_K", 9029)
            blnError = True
        End If

        If Not blnError Then
            If lclsDoc_Pay.Find(nTypesupport, sClient, nDocument, NumNull, dtmNull, dtmNull, NumNull) Then
                If lclsDoc_Pay.nStatus = 2 Or lclsDoc_Pay.nStatus = 3 Then
                    Call lclsErrors.ErrorMessage("NC001_K", 10503)
                Else
                    Call lclsErrors.ErrorMessage("NC001_K", 10502)
                End If
            End If
        End If

        insValNC001_K = lclsErrors.Confirm


insValNC001_K_Err:
        If Err.Number Then
            insValNC001_K = "insValNC001_K: " & Err.Description
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDoc_Pay = Nothing
    End Function

    '% insValNC002_K:Validacion de la Ventana NC002_K
    Public Function insValNC002_K(ByVal nTypesupport As Integer, ByVal sClient As String, ByVal nDocument As Double, ByVal nStatus As Integer, ByVal dStatus1 As Date, ByVal dStatus2 As Date, ByVal nUsercode As Integer) As String

        Dim lclsErrors As Object
        lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")
        Dim lclsDoc_Pay As eClaim.Document_Pay


        On Error GoTo insValNC002_K_Err

        lclsDoc_Pay = New eClaim.Document_Pay
        lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")

        insValNC002_K = CStr(True)


        '+Validacion del Tipo de documento.
        If ((nTypesupport = NumNull Or nTypesupport <= 0) And (Trim(sClient) = String.Empty) And (nDocument = NumNull Or nDocument <= 0) And (nStatus = NumNull Or nStatus <= 0) And (dStatus1 = dtmNull) And (dStatus2 = dtmNull) And (nUsercode = NumNull Or nUsercode <= 0)) Then

            Call lclsErrors.ErrorMessage("NC002_K", 3951)
        End If


        insValNC002_K = lclsErrors.Confirm
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDoc_Pay = Nothing

insValNC002_K_Err:
        If Err.Number Then
            insValNC002_K = "insValNC002_K: " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '% insValNC003:Validacion de la Ventana NC003_K
    Public Function insValNC003_K(ByVal nAction As Integer, ByVal nClaim As Double, ByVal nServ_order As Integer, ByVal nDocument As Double, ByVal nStatus As Integer) As String

        Dim lrecinsValNC003 As Object
        Dim lstrErrors As String
        Dim lclsErrors As Object

        On Error GoTo insValNC003_err

        lrecinsValNC003 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
        lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")

        With lrecinsValNC003
            .StoredProcedure = "INSNC003PKG.INSVALNC003"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nStatus", nStatus, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            lstrErrors = .Parameters("Arrayerrors").Value
        End With
        lclsErrors.ErrorMessage("NC003", , , , , , lstrErrors)
        insValNC003_K = lclsErrors.Confirm

insValNC003_err:
        If Err.Number Then
            insValNC003_K = "insValNC003_K: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecinsValNC003 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValNC003 = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function

    '% insValNC005_K:Validacion de la Ventana NC005_K
    Public Function insValNC005_K(ByVal nZone As Integer, ByVal sClient As String, ByVal nAmount As Integer, ByVal nBalance As Integer) As String
        Dim eClaim As Object
        Dim lclsErrors As Object
        Dim lclsDoc_Pay As eClaim.Document_Pay

        On Error GoTo insValNC005_K_Err

        lclsDoc_Pay = New eClaim.Document_Pay
        lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")

        insValNC005_K = CStr(True)

        If nZone = 1 Then

            '+Validacion de Rut del Proveedor.
            If Trim(sClient) = String.Empty Then
                Call lclsErrors.ErrorMessage("NC005_K", 55769)
            End If
        Else
            '+Validacion del monto de documento.
            If nAmount < 0 Or nAmount = 0 Then
                Call lclsErrors.ErrorMessage("NC005_K", 9029, , , "Total, debe ser superior a 0.")
            End If
            If nAmount > nBalance Then
                Call lclsErrors.ErrorMessage("NC005_K", 9029, , , "Total, no debe ser superior a monto de Saldo.")
            End If

        End If


        insValNC005_K = lclsErrors.Confirm
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
        'UPGRADE_NOTE: Object lclsDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDoc_Pay = Nothing

insValNC005_K_Err:
        If Err.Number Then
            insValNC005_K = "insValNC005_K: " & Err.Description
        End If
        On Error GoTo 0
    End Function

    '*insPostNC001_K: Esta funcion se encarga de crear/actualizar los registros
    '*correspondientes en la tabla de documentos de pago
    Public Function insPostNC001_K(ByVal nTypesupport As Integer, ByVal sClient As String, ByVal nProvider As Integer, ByVal nDocument As Double, ByVal nAmount As Double, ByVal dDocument As Date, ByVal nCurrency As Integer, ByVal nUsercode As Integer) As Boolean
        Dim eClaim As Object

        Dim lclsDoc_Pay As eClaim.Document_Pay

        lclsDoc_Pay = New eClaim.Document_Pay

        On Error GoTo InsPostNC001_err


        With lclsDoc_Pay
            .nTypesupport = nTypesupport
            .sClient = sClient
            .nProvider = nProvider
            .nDocument = nDocument
            .nAmount = nAmount
            .dDocument = dDocument
            .nCurrency = nCurrency
            .nUsercode = nUsercode

            insPostNC001_K = .Add
        End With

        'UPGRADE_NOTE: Object lclsDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDoc_Pay = Nothing

InsPostNC001_err:
        If Err.Number Then
            insPostNC001_K = False
        End If
        On Error GoTo 0
    End Function

    '*insPostNC002_K: Esta funcion se encarga de crear/actualizar los registros
    '*correspondientes en la tabla de documentos de pago
    Public Function insPostNC002_K(ByVal nTypesupport As Integer, ByVal sClient As String, ByVal nDocument As Double, ByVal nStatus As Integer, Optional ByVal dStatus1 As Date = #12:00:00 AM#, Optional ByVal dStatus2 As Date = #12:00:00 AM#, Optional ByVal nUsercode As Integer = 0) As Boolean
        Dim eClaim As Object

        Dim lclsDoc_Pay As eClaim.Document_Pay

        lclsDoc_Pay = New eClaim.Document_Pay

        On Error GoTo insPostNC002_Err


        With lclsDoc_Pay

            If .Find(nTypesupport, sClient, nDocument, nStatus, dStatus1, dStatus2, nUsercode) Then

                Me.nTypesupport = .nTypesupport
                Me.sClient = .sClient
                Me.nDocument = .nDocument
                Me.nStatus = .nStatus

                insPostNC002_K = True
            Else
                insPostNC002_K = False
            End If

        End With
        lclsDoc_Pay = Nothing

insPostNC002_Err:
        If Err.Number Then
            insPostNC002_K = False
        End If
        On Error GoTo 0
    End Function

    '*insPostNC003_K: Esta funcion se encarga de vincular/desvincular los documentos
    '*con los siniestros/ordenes de servicio por proveedor
    Public Function insPostNC003_K(ByVal sKey As String, ByVal nTypesupport As Integer, ByVal sClient As String, ByVal nProvider As Integer, ByVal nDocument As Double, ByVal nAction As Integer, ByVal nClaim As Double, ByVal nServ_order As Integer, ByVal nUsercode As Integer) As Boolean
        Dim lrecDoc_Pay As eRemoteDB.Execute
        lrecDoc_Pay = New eRemoteDB.Execute
        On Error GoTo insPostNC003_K_Err

        '+ Define all parameters for the stored procedures 'insUpdBranprod_allow'. Generated on 06/12/2001 04:06:56 p.m.
        With lrecDoc_Pay
            .StoredProcedure = "insnc003pkg.insupdnc003"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypesupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nServ_order", nServ_order, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostNC003_K = .Run(False)
        End With

insPostNC003_K_Err:
        If Err.Number Then
            insPostNC003_K = False
        End If
        On Error GoTo 0
    End Function
    '*insPostNC005_K: Esta funcion se encarga de crear/actualizar los registros
    '*correspondientes en la tabla de documentos de pago
    Public Function insPostNC005_K(ByVal sClient As String) As Boolean
        Dim eClaim As Object

        Dim lclsDoc_Pay As eClaim.Document_Pay

        lclsDoc_Pay = New eClaim.Document_Pay

        On Error GoTo insPostNC005_Err


        If lclsDoc_Pay.Find_Provider(sClient) Then

            Me.sClient = lclsDoc_Pay.sClient
            insPostNC005_K = True
        Else
            insPostNC005_K = False
        End If

        'UPGRADE_NOTE: Object lclsDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsDoc_Pay = Nothing

insPostNC005_Err:
        If Err.Number Then
            insPostNC005_K = False
        End If
        On Error GoTo 0
    End Function

    'insPostNC002: Función que realiza el llamado a los métodos de actualización, borrado e inserción de registros
    Public Function insPostNC002(ByVal nAction As Integer, ByVal nTypesupport As Integer, ByVal sClient As String, ByVal nProvider As Integer, ByVal nDocument As Double) As Boolean
        Dim lrecDoc_Pay As eRemoteDB.Execute

        lrecDoc_Pay = New eRemoteDB.Execute

        On Error GoTo insPostNC002_Err

        '+ Define all parameters for the stored procedures 'INSNC002PKG.UPDDOC_PAY'. Generated on 06/12/2001 04:06:56 p.m.

        With lrecDoc_Pay
            .StoredProcedure = "INSNC002PKG.UPDDOC_PAY"
            .Parameters.Add("nAction", nAction, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nTypesupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nProvider", nProvider, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            insPostNC002 = .Run(False)
        End With

insPostNC002_Err:
        If Err.Number Then
            insPostNC002 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecDoc_Pay may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecDoc_Pay = Nothing
    End Function

    'insPostNC005: Función que realiza el llamado a los métodos de actualización, borrado e inserción de registros
    Public Function insPostNC005(ByVal nAction As Integer, ByVal nTyp_acco As Integer, ByVal sType_acc As String, ByVal sClient As String, ByVal nCurrency As Integer, Optional ByVal nType_move As Integer = 0, Optional ByVal nUsercode As Integer = 0, Optional ByVal nDocument As Double = 0, Optional ByVal nTypesupport As Integer = 0, Optional ByVal nUpd As Integer = 0, Optional ByVal sKey As String = "") As Boolean

        Dim lrecMove_Acc As eRemoteDB.Execute

        lrecMove_Acc = New eRemoteDB.Execute

        On Error GoTo insPostNC005_Err

        Select Case nAction
            Case 1
                '+ Define all parameters for the stored procedures 'INSNC005PKG.CREAMove_Acc'. Generated on 02/06/2008

                With lrecMove_Acc
                    .StoredProcedure = "INSNC005PKG.CREAMove_Acc"
                    .Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nType_move", nType_move, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 5, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 30, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCheque", nCheque, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

                    insPostNC005 = .Run(False)
                    If insPostNC005 Then
                        nCheque = .Parameters("nCheque").Value
                    End If
                End With

            Case 2
                '+ Define all parameters for the stored procedures 'INSNC005PKG.UPDREGMove_Acc'. Generated on 04/06/2008

                With lrecMove_Acc
                    .StoredProcedure = "INSNC005PKG.UPDREGMove_Acc"
                    .Parameters.Add("nTyp_acco", nTyp_acco, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sType_acc", sType_acc, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 1, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("sClient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nCurrency", nCurrency, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nDocument", nDocument, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nTypesupport", nTypesupport, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 5, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
                    .Parameters.Add("nUpd", nUpd, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 2, 0, 2, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


                    insPostNC005 = .Run(False)
                End With

        End Select


insPostNC005_Err:
        If Err.Number Then
            insPostNC005 = False
        End If
        On Error GoTo 0
        'UPGRADE_NOTE: Object lrecMove_Acc may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecMove_Acc = Nothing
    End Function

    'UPGRADE_NOTE: Class_Initialize was upgraded to Class_Initialize_Renamed. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
    Private Sub Class_Initialize_Renamed()


        nTypesupport = NumNull
        sClient = String.Empty
        nDocument = NumNull
        nProvider = NumNull
        nAmount = NumNull
        dDocument = dtmNull
        nStatus = NumNull
        sOpertype = String.Empty
        dStatdate = dtmNull
        dNulldate = dtmNull
        nUsercode = NumNull
        dCompdate = dtmNull
        nClaim = NumNull
        nCurrency = NumNull
        nServ_order = NumNull
        sCliename = String.Empty

    End Sub
    Public Sub New()
        MyBase.New()
        Class_Initialize_Renamed()
    End Sub

    Public Function insValNC004K(ByVal sCodispl As String, ByVal sClient As String, ByVal nClaim As Double) As String
        Dim rdbParamOutput As Object
        Dim rdbDouble As Object
        Dim rdbParamNullable As Object
        Dim rdbVarChar As Object
        Dim rdbParamInput As Object
        Dim lrecinsValNC004K As Object
        Dim lstrErrors As String
        Dim lclsErrors As Object

        On Error GoTo insValNC004K_err

        lrecinsValNC004K = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
        lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")

        With lrecinsValNC004K
            .StoredProcedure = "INSNC004PKG.INSVALNC004K"
            .Parameters.Add("sclient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 4000, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)

            .Run(False)
            lstrErrors = .Parameters("Arrayerrors").Value
        End With
        lclsErrors.ErrorMessage("NC004", , , , , , lstrErrors)
        insValNC004K = lclsErrors.Confirm

insValNC004K_err:
        If Err.Number Then
            insValNC004K = "insValC004K: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecinsValNC004K may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValNC004K = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function


    Public Function insPostNC004K(ByVal sCodispl As String, ByVal sKey As String, ByVal sClient As String, ByVal nClaim As Double, ByVal nUsercode As Short) As Boolean
        Dim lrecDoc_Pay As eRemoteDB.Execute
        lrecDoc_Pay = New eRemoteDB.Execute
        On Error GoTo insPostNC004_K_Err

        With lrecDoc_Pay
            .StoredProcedure = "INSNC004PKG.INSPOSTNC004K"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("sclient", sClient, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 14, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nClaim", nClaim, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            insPostNC004K = .Run(False)
        End With

insPostNC004_K_Err:
        If Err.Number Then
            insPostNC004K = False
        End If
        On Error GoTo 0
    End Function

    Public Function insPostNC004Upd(ByVal sCodispl As String, ByVal sKey As String, ByVal nIndex As Short, ByVal nsel As String, ByVal nUsercode As Short) As Boolean

        Dim lrecDoc_Pay As eRemoteDB.Execute
        lrecDoc_Pay = New eRemoteDB.Execute
        On Error GoTo insPostNC004Upd_Err

        With lrecDoc_Pay
            .StoredProcedure = "INSNC004PKG.insPostNC004Upd"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nindex", nIndex, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 22, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nsel", nsel, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 20, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)


            insPostNC004Upd = .Run(False)
        End With

insPostNC004Upd_Err:
        If Err.Number Then
            insPostNC004Upd = False
        End If
        On Error GoTo 0
    End Function


    Public Function insValNC004(ByVal sCodispl As String, ByVal sKey As Object) As String
        Dim rdbParamOutput As Object
        Dim rdbParamNullable As Object
        Dim rdbVarChar As Object
        Dim rdbParamInput As Object
        Dim lrecinsValNC004 As Object
        Dim lstrErrors As String
        Dim lclsErrors As Object

        On Error GoTo insValNC004_err

        lrecinsValNC004 = eRemoteDB.NetHelper.CreateClassInstance("eRemoteDB.Execute")
        lclsErrors = eRemoteDB.NetHelper.CreateClassInstance("eFunctions.Errors")

        With lrecinsValNC004
            .StoredProcedure = "INSNC004PKG.INSVALNC004"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("Arrayerrors", String.Empty, eRemoteDB.Parameter.eRmtDataDir.rdbParamOutput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)




            .Run(False)
            lstrErrors = .Parameters("Arrayerrors").Value
        End With
        lclsErrors.ErrorMessage("NC004", , , , , , lstrErrors)
        insValNC004 = lclsErrors.Confirm

insValNC004_err:
        If Err.Number Then
            insValNC004 = "insValC004: " & Err.Description
        End If
        'UPGRADE_NOTE: Object lrecinsValNC004 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lrecinsValNC004 = Nothing
        'UPGRADE_NOTE: Object lclsErrors may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
        lclsErrors = Nothing
    End Function




    Public Function insPostNC004(ByVal sCodispl As String, ByVal sKey As String, ByVal nUsercode As Short) As Boolean

        Dim lrecDoc_Pay As eRemoteDB.Execute
        lrecDoc_Pay = New eRemoteDB.Execute
        On Error GoTo insPostNC004_Err

        With lrecDoc_Pay
            .StoredProcedure = "INSNC004PKG.insPostNC004"
            .Parameters.Add("sKey", sKey, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbVarchar, 20, 0, 0, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)
            .Parameters.Add("nUsercode", nUsercode, eRemoteDB.Parameter.eRmtDataDir.rdbParamInput, eRemoteDB.Parameter.eRmtDataType.rdbInteger, 10, 0, 10, eRemoteDB.Parameter.eRmtDataAttrib.rdbParamNullable)



            insPostNC004 = .Run(False)
        End With

insPostNC004_Err:
        If Err.Number Then
            insPostNC004 = False
        End If
        On Error GoTo 0
    End Function


End Class






